#if EASEKIT_3
using EaseKit;

namespace ClockKit {
    /// <summary>
    /// A fixed duration animation that eases between two values based on a given easing function.
    /// </summary>
    public readonly struct CKEasingAnimation<Value> : ICKFiniteAnimation<Value> {
        public float Duration { get; }
        public readonly EasingUtility.Function easingFunction;
        public readonly Value start;
        public readonly Value end;
        public readonly IInterpolator<Value> interpolator;

        public CKEasingAnimation(
            Easing easing,
            float duration,
            in Value start,
            in Value end
        ) : this(EasingUtility.CreateInterpolator<Value>(), easing.GetFunction(), duration, start, end) { }

        public CKEasingAnimation(
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end
        ) : this(interpolator, easing.GetFunction(), duration, start, end) { }

        public CKEasingAnimation(
            in EasingUtility.Function easingFunction,
            float duration,
            in Value start,
            in Value end
        ) : this(EasingUtility.CreateInterpolator<Value>(), easingFunction, duration, start, end) { }

        public CKEasingAnimation(
            in IInterpolator<Value> interpolator,
            EasingUtility.Function easingFunction,
            float duration,
            in Value start,
            in Value end
        ) {
            this.Duration = duration;
            this.easingFunction = easingFunction;
            this.start = start;
            this.end = end;
            this.interpolator = interpolator;
        }

        public Value Evaluate(float localTime, float percent)
            => EasingUtility.Evaluate(interpolator, easingFunction, start, end, percent);
    }
}
#endif