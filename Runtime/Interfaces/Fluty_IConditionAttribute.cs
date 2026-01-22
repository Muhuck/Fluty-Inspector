namespace Fluty.Inspector
{
    /// <summary>
    /// Marker interface for attributes that depend on a boolean condition.
    /// Used by Fluty Inspector to evaluate visibility or enabled state.
    /// </summary>
    public interface Fluty_IConditionAttribute
    {
        /// <summary>
        /// Name of field / property / method used as condition
        /// </summary>
        string Condition { get; }

        /// <summary>
        /// Whether the result should be inverted (!condition)
        /// </summary>
        bool Inverse { get; }
    }
}
