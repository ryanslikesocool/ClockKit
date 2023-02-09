namespace ClockKit {
    public interface ITimer {
        bool IsComplete { get; }
        float StartTime { get; }

        bool Update(in Information information);
    }
}