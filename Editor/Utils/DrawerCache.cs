#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace Fluty.Inspector.Editor
{
    public static class DrawerCache
    {
        private static readonly Dictionary<string, bool> conditionCache = new();

        public static bool TryGet(string key, out bool value)
        {
            return conditionCache.TryGetValue(key, out value);
        }

        public static void Set(string key, bool value)
        {
            conditionCache[key] = value;
        }

        public static string GetKey(SerializedProperty property, string condition)
        {
            return $"{property.serializedObject.targetObject.GetInstanceID()}:{property.propertyPath}:{condition}";
        }

        public static void Clear()
        {
            conditionCache.Clear();
        }
    }
}
#endif
