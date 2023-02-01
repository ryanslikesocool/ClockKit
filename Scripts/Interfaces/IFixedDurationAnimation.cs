using UnityEngine;

namespace ClockKit {
    public interface IFixedDurationAnimation<Value> : IAnimation<Value> {
        float Duration { get; }
    }
}