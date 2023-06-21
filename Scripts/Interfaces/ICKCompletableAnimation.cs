namespace ClockKit {
    /// <summary>
    /// Refines <see cref="ICKAnimation"/> for potentially indefinite animations that must provide completion implementation.
    /// </summary>
    /// <typeparam name="Value">The type of value to animate.</typeparam>
    /// <seealso cref="ICKAnimation"/>
    public interface ICKCompletableAnimation<Value> : ICKAnimation<Value> {
        bool IsComplete { get; }
    }
}