#if EASEKIT_3_1
using EaseKit;

namespace ClockKit {
    public struct SpringAnimation<Value> : ICompletableAnimation<Value> {
        public readonly Value start;
        public readonly Value end;
        private SpringInterpolator<Value> interpolator; // { get; private set; } does not work here

        public bool IsComplete => interpolator.IsComplete;

        public SpringAnimation(
           in Value start,
           in Value end
        ) : this(Spring.Default, 0, start, end) { }

        public SpringAnimation(
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(Spring.Default, initialVelocity, start, end) { }

        public SpringAnimation(
            in Spring spring,
            Value start,
            Value end
        ) : this(spring, EasingUtility.CreateInterpolator<Value>(), 0, start, end) { }

        public SpringAnimation(
            in Spring spring,
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(spring, EasingUtility.CreateInterpolator<Value>(), initialVelocity, start, end) { }

        public SpringAnimation(
            in IInterpolator<Value> subinterpolator,
            in Value start,
            in Value end
        ) : this(Spring.Default, subinterpolator, 0, start, end) { }

        public SpringAnimation(
            in IInterpolator<Value> subinterpolator,
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(Spring.Default, subinterpolator, initialVelocity, start, end) { }

        public SpringAnimation(
            in Spring spring,
            in IInterpolator<Value> subinterpolator,
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(new SpringInterpolator<Value>(spring, subinterpolator, initialVelocity), start, end) { }

        public SpringAnimation(
            in SpringInterpolator<Value> interpolator,
            in Value start,
            in Value end
        ) {
            this.start = start;
            this.end = end;
            this.interpolator = interpolator;
        }

        public Value Evaluate(float percent)
            => interpolator.Evaluate(start, end, percent);
    }
}
#endif