using UnityEngine;

namespace ClockKit {
    /// <summary>
    /// A fixed duration animation that evaluates a Unity AnimationCurve every step.
    /// </summary>
    public readonly struct AnimationCurveAnimation : IFixedDurationAnimation<float> {
        public float Duration { get; }
        public readonly AnimationCurve animationCurve;
        private readonly float startTime;

        public AnimationCurveAnimation(in AnimationCurve animationCurve) {
            startTime = animationCurve.keys[0].time;
            this.Duration = animationCurve.keys[animationCurve.length - 1].time - animationCurve.keys[0].time;
            this.animationCurve = animationCurve;
        }

        public float Evaluate(float localTime, float percent)
            => animationCurve.Evaluate(startTime + localTime);
    }
}