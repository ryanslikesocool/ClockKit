// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using System;

namespace ClockKit {
	public static partial class CKClock {
		// MARK: Update Until

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		// MARK: Update Seconds

		public static CKKey Update(
			CKQueue queue,
			float seconds,
			in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKFiniteUpdatingTimer(
				startTime: Time.time,
				seconds: seconds,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			float seconds,
			in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, seconds: seconds, onUpdate, onComplete);

		// MARK: Update Frames

		public static CKKey Update(
			CKQueue queue,
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKFrameUpdatingTimer(
				startTime: Time.time,
				frames: frames,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.SimpleCompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameUpdatingTimer(
				startTime: Time.time,
				frames: frames,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, frames: frames, onUpdate, onComplete);

		public static CKKey Update(
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.SimpleCompletionCallback onComplete
		)
			=> Update(CKQueue.Default, frames: frames, onUpdate, onComplete);

		// MARK: Delay

		/// <summary>
		/// Delays a function call for a single update cycle (normally a single frame).
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(queue, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update cycle (normally a single frame).
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(queue, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update cycle (normally a single frame).
		/// </summary>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update cycle (normally a single frame).
		/// </summary>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames: 1, onComplete);

		// MARK: Delay Frames

		public static CKKey Delay(
			CKQueue queue,
			int frames,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameDelayingTimer(
				startTime: Time.time,
				frames: frames,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			int frames,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameDelayingTimer(
				startTime: Time.time,
				frames: frames,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			int frames,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames, onComplete);

		public static CKKey Delay(
			int frames,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames, onComplete);

		// MARK: Delay Until

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteDelayingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		)
			=> Delay(CKQueue.Default, until, onComplete);

		// MARK: Delay For

		public static CKKey Delay(
			CKQueue queue,
			float seconds,
			in CKFiniteDelayingTimer.CompletionCallback onComplete
		) {
			ICKTimer timer = new CKFiniteDelayingTimer(
				startTime: Time.time,
				duration: seconds,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			float seconds,
			in CKFiniteDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, seconds, onComplete);

		// MARK: Animate

		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKFiniteAnimation<Value> {
			ICKTimer timer = new CKFiniteAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKFiniteAnimation<Value> {
			ICKTimer timer = new CKFiniteAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKFiniteAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKFiniteAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);

		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value> {
			ICKTimer timer = new CKCompletableAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value> {
			ICKTimer timer = new CKCompletableAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);

		// MARK: Sequence

		public static CKKey Sequence(
			CKQueue queue,
			in Func<ICKTimer>[] timerBuilders,
			in CKSequenceTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKSequenceTimer(
				startTime: Time.time,
				timerBuilders: timerBuilders,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Sequence(
			in Func<ICKTimer>[] timerBuilders,
			in CKSequenceTimer.CompletionCallback onComplete = null
		)
			=> Sequence(CKQueue.Default, timerBuilders, onComplete);
	}
}