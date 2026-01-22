using System.Collections.Generic;

namespace Fluty.Editor
{
    public static class GroupResolver
    {
        public static string[] Split(string path)
        {
            return path.Split('/');
        }

        public static string BuildPath(string[] segments, int depth)
        {
            return string.Join("/", segments, 0, depth + 1);
        }
    }
}
