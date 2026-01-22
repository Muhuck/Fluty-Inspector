using System;

namespace Fluty.Inspector
{
    public class Fluty_FoldoutGroupAttribute : Attribute
    {
        public string Name { get; }

        public Fluty_FoldoutGroupAttribute(string name)
        {
            Name = name;
        }
    }
}

