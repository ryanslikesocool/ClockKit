namespace ClockKit {
    /// <summary>
    /// The base timer interface.
    /// </summary>
    public interface ICKTimer {
        bool IsComplete { get; }
        float StartTime { get; }

        bool OnUpdate(in CKClockInformation information);
    }
}