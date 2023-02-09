namespace ClockKit {
    public interface IAnimation<Value> {
        Value Evaluate(float percent);
    }
}