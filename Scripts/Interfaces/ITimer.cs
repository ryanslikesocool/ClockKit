namespace ClockKit {
    /// <summary>
    /// The base timer interface.
    /// </summary>
    public interface ITimer {
        bool IsComplete { get; }
        float StartTime { get; }

        bool OnUpdate(in ClockInformation information);
    }
}