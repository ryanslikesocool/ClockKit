#if EASEKIT_3
using EaseKit;
using Foundation;
using UnityEngine;

namespace ClockKit {
    public static partial class CKClock {
        public static CKKey Ease<Value>(
            CKQueue queue,
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.CompletionCallback onComplete = null
        ) {
            EasingAnimation<Value> animation = new EasingAnimation<Value>(
                interpolator: interpolator,
                easing: easing,
                duration: duration,
                start: start,
                end: end
            );
            ITimer timer = new FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>(
                startTime: Time.time,
                animation: animation,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Ease<Value>(
            in IInterpolator<Value> interpolator,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                CKQueue.Default,
                interpolator,
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static CKKey Ease<Value>(
            CKQueue queue,
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                queue,
                EasingUtility.CreateInterpolator<Value>(),
                easing,
                duration,
                start,
                end,
                onUpdate,
                onComplete
            );

        public static CKKey Ease<Value>(
            Easing easing,
            float duration,
            in Value start,
            in Value end,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, IFiniteAnimation<Value>>.CompletionCallback onComplete = null
        )
            => Ease(
                CKQueue.Default,
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