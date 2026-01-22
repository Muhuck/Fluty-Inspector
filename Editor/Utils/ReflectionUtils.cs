#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Fluty.Editor
{
    public static class ReflectionUtils
    {
        private static readonly Dictionary<string, MemberInfo> memberCache = new();

        public static bool GetConditionValue(object target, string memberName)
        {
            if (target == null || string.IsNullOrEmpty(memberName))
                return true;

            var type = target.GetType();
            string key = type.FullName + "." + memberName;

            if (!memberCache.TryGetValue(key, out MemberInfo member))
            {
                member =
                    (MemberInfo)type.GetField(memberName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ??
                    type.GetProperty(memberName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ??
                    (MemberInfo)type.GetMethod(memberName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                        null, Type.EmptyTypes, null);

                memberCache[key] = member;
            }

            if (member == null)
            {
                Debug.LogError($"[FlutyInspector] Condition member '{memberName}' not found on {type.Name}");
                return true;
            }

            return member switch
            {
                FieldInfo f when f.FieldType == typeof(bool)
                    => (bool)f.GetValue(target),

                PropertyInfo p when p.PropertyType == typeof(bool)
                    => (bool)p.GetValue(target),

                MethodInfo m when m.ReturnType == typeof(bool)
                    => (bool)m.Invoke(target, null),

                _ => true
            };
        }

        public static MethodInfo[] GetAllMethods(object target)
        {
            return target.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );
        }

        public static List<FlutyMemberInfo> CollectMembers(System.Object target)
        {
            var list = new List<FlutyMemberInfo>();
            var type = target.GetType();

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var field in type.GetFields(flags))
            {
                if (field.IsPublic || field.GetCustomAttribute<SerializeField>() != null)
                {
                    list.Add(new FlutyMemberInfo { Field = field });
                }
            }

            foreach (var method in type.GetMethods(flags))
            {
                if (method.GetCustomAttribute<Fluty.Inspector.Fluty_ButtonAttribute>() != null)
                {
                    list.Add(new FlutyMemberInfo { Method = method });
                }
            }

            return list;
        }
    }
}
#endif
