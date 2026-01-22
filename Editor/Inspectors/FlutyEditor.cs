#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Fluty.Inspector;

namespace Fluty.Editor
{
    /// <summary>
    /// Base editor for Fluty Inspector.
    /// Inherit this editor to enable Fluty features.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FlutyEditor<T> : UnityEditor.Editor where T : UnityEngine.Object
    {
        private readonly Dictionary<string, bool> foldouts = new();
        private static readonly Dictionary<MethodInfo, object[]> parameterCache
            = new Dictionary<MethodInfo, object[]>();
        private readonly Dictionary<string, bool> foldoutStates = new();


        public override void OnInspectorGUI()
        {
            if (!FlutyUtility.ShouldUseFluty(target))
            {
                DrawDefaultInspector();
                return;
            }
            serializedObject.Update();
            var members = ReflectionUtils.CollectMembers(target);
            var tree = BuildGroupTree(members);
            DrawGroupNode(tree);
            serializedObject.ApplyModifiedProperties();
        }

        #region Drawing

        private void DrawGroups(List<FlutyMemberInfo> members)
        {
            var grouped = new Dictionary<string, List<FlutyMemberInfo>>();

            foreach (var m in members)
            {
                var group = m.GetAttribute<Fluty_FoldoutGroupAttribute>()?.Name ?? "Default";

                if (!grouped.ContainsKey(group))
                    grouped[group] = new List<FlutyMemberInfo>();

                grouped[group].Add(m);
            }

            foreach (var kv in grouped)
            {
                foldouts.TryAdd(kv.Key, true);
                foldouts[kv.Key] = EditorGUILayout.Foldout(foldouts[kv.Key], kv.Key, true);

                if (!foldouts[kv.Key]) continue;

                EditorGUI.indentLevel++;
                foreach (var member in kv.Value)
                    DrawMember(member);
                EditorGUI.indentLevel--;
            }
        }

        private void DrawGroupNode(FlutyGroupNode node)
        {
            if (node.Condition != null)
            {
                bool visible = ConditionResolver.Evaluate(target, node.Condition);
                if (!visible)
                {
                    CollapseRecursively(node);
                    return;
                }
            }

            if (node.Name != "ROOT")
            {
                if (!foldoutStates.TryGetValue(node.FullPath, out bool expanded))
                    expanded = true;

                expanded = EditorGUILayout.Foldout(expanded, node.Name, true);
                foldoutStates[node.FullPath] = expanded;

                if (!expanded)
                    return;

                EditorGUI.indentLevel++;
            }

            foreach (var m in node.Members)
                DrawMember(m);

            foreach (var child in node.Children.Values)
                DrawGroupNode(child);

            if (node.Name != "ROOT")
                EditorGUI.indentLevel--;
        }


        private void DrawMember(FlutyMemberInfo member)
        {
            var showIf = member.GetAttribute<Fluty_ShowIfAttribute>();
            if (showIf != null)
            {
                bool visible = ConditionResolver.Evaluate(target, showIf);
                if (!visible)
                    return; // hide
            }

            bool disabled = false;
            var disableIf = member.GetAttribute<Fluty_DisableIfAttribute>();
            if (disableIf != null)
            {
                disabled = ConditionResolver.Evaluate(target, disableIf);
            }

            EditorGUI.BeginDisabledGroup(disabled);

            if (member.IsField)
            {
                var prop = serializedObject.FindProperty(member.Field.Name);
                if (prop != null)
                    EditorGUILayout.PropertyField(prop, true);
            }
            else if (member.IsMethod)
            {
                DrawButton(member.Method);
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawButton(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<Fluty_ButtonAttribute>();
            var parameters = method.GetParameters();

            if (!parameterCache.TryGetValue(method, out var values))
            {
                values = new object[parameters.Length];
                parameterCache[method] = values;
            }

            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (attr.Expanded && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                    values[i] = FlutyParameterDrawer.Draw(parameters[i], values[i]);
            }

            var label = string.IsNullOrEmpty(attr.Label)
                ? ObjectNames.NicifyVariableName(method.Name)
                : attr.Label;

            if (GUILayout.Button(label))
                method.Invoke(target, values);

            EditorGUILayout.EndVertical();
        }

        private FlutyGroupNode BuildGroupTree(List<FlutyMemberInfo> members)
        {
            var root = new FlutyGroupNode
            {
                Name = "ROOT",
                FullPath = string.Empty
            };

            foreach (var m in members)
            {
                var groupAttr = m.GetAttribute<Fluty_FoldoutGroupAttribute>();
                var path = groupAttr?.Name ?? "Default";
                var parts = path.Split('/');

                var current = root;
                string currentPath = "";

                foreach (var part in parts)
                {
                    currentPath = string.IsNullOrEmpty(currentPath)
                        ? part
                        : $"{currentPath}/{part}";

                    if (!current.Children.TryGetValue(part, out var child))
                    {
                        child = new FlutyGroupNode
                        {
                            Name = part,
                            FullPath = currentPath
                        };

                        // ⬇️ INHERIT CONDITION FROM FIRST MEMBER
                        var showIf = m.GetAttribute<Fluty_ShowIfAttribute>();
                        if (showIf != null)
                            child.Condition = showIf;

                        current.Children.Add(part, child);
                    }

                    current = child;
                }

                current.Members.Add(m);
            }

            return root;
        }

        #endregion

        #region Parameter Handling

        private object[] CreateDefaultParameters(ParameterInfo[] parameters)
        {
            var values = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = parameters[i].HasDefaultValue
                    ? parameters[i].DefaultValue
                    : GetDefaultValue(parameters[i].ParameterType);
            }

            return values;
        }

        private object GetDefaultValue(Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type == typeof(Color))
                return Color.white;

            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

        private object DrawParameterField(ParameterInfo info, object value)
        {
            var type = info.ParameterType;
            string label = ObjectNames.NicifyVariableName(info.Name);

            if (type == typeof(int))
                return EditorGUILayout.IntField(label, value != null ? (int)value : 0);

            if (type == typeof(float))
                return EditorGUILayout.FloatField(label, value != null ? (float)value : 0f);

            if (type == typeof(bool))
                return EditorGUILayout.Toggle(label, value != null && (bool)value);

            if (type == typeof(string))
                return EditorGUILayout.TextField(label, value as string ?? string.Empty);

            if (type == typeof(Vector2))
                return EditorGUILayout.Vector2Field(label, value != null ? (Vector2)value : Vector2.zero);

            if (type == typeof(Vector3))
                return EditorGUILayout.Vector3Field(label, value != null ? (Vector3)value : Vector3.zero);

            if (type == typeof(Vector4))
                return EditorGUILayout.Vector4Field(label, value != null ? (Vector4)value : Vector4.zero);

            if (type == typeof(Color))
                return EditorGUILayout.ColorField(label, value != null ? (Color)value : Color.white);

            if (type.IsEnum)
                return EditorGUILayout.EnumPopup(label, (Enum)(value ?? Activator.CreateInstance(type)));

            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                return EditorGUILayout.ObjectField(label, value as UnityEngine.Object, type, true);

            EditorGUILayout.HelpBox(
                $"Fluty Inspector does not support parameter type '{type.Name}'",
                MessageType.Warning);

            return value;
        }

        private void CollapseRecursively(FlutyGroupNode node)
        {
            if (!string.IsNullOrEmpty(node.FullPath))
                foldoutStates[node.FullPath] = false;

            foreach (var child in node.Children.Values)
                CollapseRecursively(child);
        }

        #endregion
    }

    class FlutyGroupNode
    {
        public string Name;
        public string FullPath;
        public Dictionary<string, FlutyGroupNode> Children = new();
        public List<FlutyMemberInfo> Members = new();
        public Fluty_IConditionAttribute Condition;
    }
}
#endif
