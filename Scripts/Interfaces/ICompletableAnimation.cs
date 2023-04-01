namespace ClockKit {
    public interface ICompletableAnimation<Value> : IAnimation<Value> {
        bool IsComplete { get; }
    }
}