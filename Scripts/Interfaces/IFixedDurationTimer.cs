using UnityEngine;

namespace ClockKit {
    public interface IFixedDurationTimer : ITimer {
        float Duration { get; }
    }
}