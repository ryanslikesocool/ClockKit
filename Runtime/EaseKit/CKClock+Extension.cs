// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if EASEKIT_3
using EaseKit;
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
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.CompletionCallback onComplete = null
		) {
			CKEasingAnimation<Value> animation = new CKEasingAnimation<Value>(
				interpolator: interpolator,
				easing: easing,
				duration: duration,
				start: start,
				end: end
			);
			ICKTimer timer = new CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Ease<Value>(
			CKQueue queue,
			in IInterpolator<Value> interpolator,
			Easing easing,
			float duration,
			in Value start,
			in Value end,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.SimpleCompletionCallback onComplete
		) {
			CKEasingAnimation<Value> animation = new CKEasingAnimation<Value>(
				interpolator: interpolator,
				easing: easing,
				duration: duration,
				start: start,
				end: end
			);
			ICKTimer timer = new CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>(
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
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.CompletionCallback onComplete = null
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
			in IInterpolator<Value> interpolator,
			Easing easing,
			float duration,
			in Value start,
			in Value end,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.SimpleCompletionCallback onComplete
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
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.CompletionCallback onComplete = null
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
			CKQueue queue,
			Easing easing,
			float duration,
			in Value start,
			in Value end,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.SimpleCompletionCallback onComplete
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
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.CompletionCallback onComplete = null
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

		public static CKKey Ease<Value>(
			Easing easing,
			float duration,
			in Value start,
			in Value end,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, ICKFiniteAnimation<Value>>.SimpleCompletionCallback onComplete
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