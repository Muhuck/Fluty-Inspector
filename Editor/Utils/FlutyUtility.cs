using System.Reflection;
using UnityEngine;

namespace Fluty.Editor
{
    public static class FlutyUtility
    {
        public static bool ShouldUseFluty(Object target)
        {
            var type = target.GetType();
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var field in type.GetFields(flags))
                if (HasFlutyAttribute(field)) return true;

            foreach (var method in type.GetMethods(flags))
                if (HasFlutyAttribute(method)) return true;

            return false;
        }

        private static bool HasFlutyAttribute(MemberInfo member)
        {
            foreach (var attr in member.GetCustomAttributes())
            {
                if (attr.GetType().Namespace == "Fluty.Inspector")
                    return true;
            }
            return false;
        }
    }
}
