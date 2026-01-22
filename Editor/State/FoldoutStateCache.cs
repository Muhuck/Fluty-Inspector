using System.Collections.Generic;

namespace Fluty.Editor
{
    public static class FoldoutStateCache
    {
        private static readonly Dictionary<string, bool> states = new();

        public static bool Get(string path)
        {
            if (!states.ContainsKey(path))
                states[path] = true;

            return states[path];
        }

        public static void Set(string path, bool value)
        {
            states[path] = value;
        }
    }
}
