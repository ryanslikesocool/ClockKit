namespace ClockKit {
    public interface ITimer {
        bool IsComplete { get; }
        float StartTime { get; }

        bool OnUpdate(in ClockInformation information);
    }
}