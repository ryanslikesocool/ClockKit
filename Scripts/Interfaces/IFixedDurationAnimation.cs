namespace ClockKit {
    /// <summary>
    /// Refines <see cref="IAnimation"/> for animations with a fixed duration.
    /// </summary>
    /// <typeparam name="Value">The type of value to animate.</typeparam>
    /// <seealso cref="IAnimation"/>
    public interface IFixedDurationAnimation<Value> : IAnimation<Value> {
        float Duration { get; }
    }
}