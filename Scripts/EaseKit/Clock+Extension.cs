#if EASEKIT_3
using EaseKit;
using Foundation;
using UnityEngine;

namespace ClockKit {
    public static partial class Clock {
        public static UUID Ease<Value>(
            Queue queue,
            in UUID key,
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        ) {
            EasingAnimation<Value> animation = new EasingAnimation<Value>(
                interpolator: interpolator,
                easing: easing,
                duration: duration,
                start: start,
                end: end
            );
            ITimer timer = new FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>(
                startTime: Time.time,
                animation: animation,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return Clock.StartTimer(queue, key, timer);
        }

        public static UUID Ease<Value>(
            in UUID key,
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                Queue.Default,
                key,
                interpolator,
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            Queue queue,
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                queue,
                UUID.Create(),
                interpolator,
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                Queue.Default,
                UUID.Create(),
                interpolator,
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            Queue queue,
            in UUID key,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                queue,
                key,
                EasingUtility.CreateInterpolator<Value>(),
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            in UUID key,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                Queue.Default,
                key,
                EasingUtility.CreateInterpolator<Value>(),
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            Queue queue,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                queue,
                UUID.Create(),
                EasingUtility.CreateInterpolator<Value>(),
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static UUID Ease<Value>(
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.UpdateCallback onUpdate,
            FixedDurationAnimationUpdatingTimer<Value, IFixedDurationAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                Queue.Default,
                UUID.Create(),
                EasingUtility.CreateInterpolator<Value>(),
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );
    }
}
#endif