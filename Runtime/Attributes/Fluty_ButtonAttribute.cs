using System;

namespace Fluty.Inspector
{
    /// <summary>
    /// Draws a clickable button in the Unity Inspector
    /// for methods without parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Fluty_ButtonAttribute : Attribute
    {
        /// <summary>
        /// Custom label displayed on the button.
        /// If null or empty, method name will be used.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Whether parameters are shown in inspector.
        /// </summary>
        public bool Expanded { get; }

        public Fluty_ButtonAttribute()
        {
            Label = null;
            Expanded = true;
        }

        public Fluty_ButtonAttribute(string label)
        {
            Label = label;
            Expanded = true;
        }

        public Fluty_ButtonAttribute(string label, bool expanded)
        {
            Label = label;
            Expanded = expanded;
        }
    }
}
