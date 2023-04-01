namespace ClockKit {
    /// <summary>
    /// Refines <see cref="ITimer"/> for timers to be complete after a set duration.
    /// </summary>
    /// <seealso cref="ITimer"/>
    public interface IFixedDurationTimer : ITimer {
        float Duration { get; }
    }
}