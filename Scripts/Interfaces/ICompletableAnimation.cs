namespace ClockKit {
    /// <summary>
    /// Refines <see cref="IAnimation"/> for potentially indefinite animations that must provide completion implementation.
    /// </summary>
    /// <typeparam name="Value">The type of value to animate.</typeparam>
    /// <seealso cref="IAnimation"/>
    public interface ICompletableAnimation<Value> : IAnimation<Value> {
        bool IsComplete { get; }
    }
}