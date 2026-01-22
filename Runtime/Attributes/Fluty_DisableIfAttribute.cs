using System;

namespace Fluty.Inspector
{
    [AttributeUsage(
        AttributeTargets.Field |
        AttributeTargets.Property |
        AttributeTargets.Method |
        AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
        
    public sealed class Fluty_DisableIfAttribute : Attribute, Fluty_IConditionAttribute
    {
        public string Condition { get; }
        public bool Inverse { get; }

        public Fluty_DisableIfAttribute(string condition, bool inverse = false)
        {
            Condition = condition;
            Inverse = inverse;
        }
    }
}
