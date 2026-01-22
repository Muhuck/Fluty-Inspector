using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Fluty.Editor
{
    public static class FlutyParameterDrawer
    {
        public static object Draw(ParameterInfo info, object value)
        {
            var t = info.ParameterType;
            var label = ObjectNames.NicifyVariableName(info.Name);

            if (t == typeof(int)) return EditorGUILayout.IntField(label, value != null ? (int)value : 0);
            if (t == typeof(float)) return EditorGUILayout.FloatField(label, value != null ? (float)value : 0);
            if (t == typeof(bool)) return EditorGUILayout.Toggle(label, value != null && (bool)value);
            if (t == typeof(string)) return EditorGUILayout.TextField(label, value as string ?? "");
            if (t == typeof(Vector2)) return EditorGUILayout.Vector2Field(label, value != null ? (Vector2)value : Vector2.zero);
            if (t == typeof(Vector3)) return EditorGUILayout.Vector3Field(label, value != null ? (Vector3)value : Vector3.zero);
            if (t == typeof(Vector4)) return EditorGUILayout.Vector4Field(label, value != null ? (Vector4)value : Vector4.zero);
            if (t == typeof(Color)) return EditorGUILayout.ColorField(label, value != null ? (Color)value : Color.white);

            if (t.IsEnum)
                return EditorGUILayout.EnumPopup(label, (Enum)(value ?? Activator.CreateInstance(t)));

            if (typeof(UnityEngine.Object).IsAssignableFrom(t))
                return EditorGUILayout.ObjectField(label, value as UnityEngine.Object, t, true);

            EditorGUILayout.HelpBox($"Unsupported parameter: {t.Name}", MessageType.Warning);
            return value;
        }
    }
}
