using System.Reflection;

namespace Fluty.Editor
{
    public sealed class FlutyMemberInfo
    {
        public FieldInfo Field;
        public MethodInfo Method;

        public bool IsField => Field != null;
        public bool IsMethod => Method != null;

        public string Name => IsField ? Field.Name : Method.Name;

        public T GetAttribute<T>() where T : System.Attribute
        {
            return IsField
                ? Field.GetCustomAttribute<T>()
                : Method.GetCustomAttribute<T>();
        }

        public bool HasAttribute<T>() where T : System.Attribute
        {
            return GetAttribute<T>() != null;
        }
    }
}
