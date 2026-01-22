using System.Reflection;
using UnityEngine;

namespace Fluty.Inspector
{
    public static class ConditionResolver
    {
        public static bool Evaluate(Object target, Fluty_IConditionAttribute attr)
        {
            if (string.IsNullOrEmpty(attr.Condition))
                return true;

            var type = target.GetType();

            // FIELD BOOL
            var field = type.GetField(attr.Condition,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (field != null && field.FieldType == typeof(bool))
            {
                bool value = (bool)field.GetValue(target);
                return attr.Inverse ? !value : value;
            }

            // PROPERTY BOOL
            var prop = type.GetProperty(attr.Condition,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (prop != null && prop.PropertyType == typeof(bool))
            {
                bool value = (bool)prop.GetValue(target);
                return attr.Inverse ? !value : value;
            }

            // METHOD BOOL ()
            var method = type.GetMethod(attr.Condition,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, System.Type.EmptyTypes, null);

            if (method != null && method.ReturnType == typeof(bool))
            {
                bool value = (bool)method.Invoke(target, null);
                return attr.Inverse ? !value : value;
            }

            Debug.LogWarning($"[Fluty] Condition '{attr.Condition}' not found or not bool.");
            return true;
        }
    }
}
