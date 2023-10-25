using System;
using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public delegate void UpdateCallback(in CKClockInformation information);

		// MARK: - Add Delegate

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="callback">The callback to invoke every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			CKQueue queue,
			int priority,
			in UpdateCallback callback
		)
			=> CKClockController.Shared.queues[queue].AddDelegate(priority, callback);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="callback">The callback to invoke every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			int priority,
			in UpdateCallback callback
		)
			=> AddDelegate(CKQueue.Default, priority, callback);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="callback">The callback to invoke every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			CKQueue queue,
			in UpdateCallback callback
		)
			=> AddDelegate(queue, 0, callback);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="callback">The callback to invoke every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			in UpdateCallback callback
		)
			=> AddDelegate(CKQueue.Default, 0, callback);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="updatable">The object to add to the queue.  Its <see cref="ICKUpdatable.OnUpdate(in CKClockInformation)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			CKQueue queue,
			int priority,
			in ICKUpdatable updatable
		)
			=> AddDelegate(queue, priority, updatable.OnUpdate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="updatable">The object to add to the queue.  Its <see cref="ICKUpdatable.OnUpdate(in CKClockInformation)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			CKQueue queue,
			in ICKUpdatable updatable
		)
			=> AddDelegate(queue, 0, updatable.OnUpdate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="updatable">The object to add to the queue.  Its <see cref="ICKUpdatable.OnUpdate(in CKClockInformation)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			int priority,
			in ICKUpdatable updatable
		)
			=> AddDelegate(CKQueue.Default, priority, updatable.OnUpdate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="updatable">The object to add to the queue.  Its <see cref="ICKUpdatable.OnUpdate(in CKClockInformation)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddDelegate(
			in ICKUpdatable updatable
		)
			=> AddDelegate(CKQueue.Default, 0, updatable.OnUpdate);

		// MARK: - Has Delegate

		public static bool HasDelegate(
			CKQueue queue,
			in CKKey? key
		)
			=> CKClockController.Shared.queues[queue].HasDelegate(key);

		public static IEnumerable<bool> HasDelegates(
			CKQueue queue,
			IEnumerable<CKKey> keys
		)
			=> keys.Map(key => HasDelegate(queue, key));

		// MARK: - Remove Delegate

		/// <summary>
		/// Remove an update delegate from a queue with its key.
		/// </summary>
		/// <param name="queue">The queue the delegate was added to.</param>
		/// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
		public static bool RemoveDelegate(
			CKQueue queue,
			in CKKey? key
		)
			=> CKClockController.Shared.queues[queue].RemoveDelegate(key);

		/// <summary>
		/// Remove an update delegate with its key.  All queues will attempt to remove the delegate.
		/// </summary>
		/// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
		public static bool RemoveDelegate(in CKKey? key) {
			bool result = false;
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				result |= RemoveDelegate(queue, key);
			}
			return result;
		}

		/// <summary>
		/// Remove all delegates from a given queue.
		/// </summary>
		/// <param name="queue">The queue to remove all delegates from.</param>
		public static void RemoveAllDelegates(
			CKQueue queue
		)
			=> CKClockController.Shared.queues[queue].RemoveAllDelegates();

		/// <summary>
		/// Remove all delegates from every queue.
		/// </summary>
		public static void RemoveAllDelegates() {
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				RemoveAllDelegates(queue);
			}
		}

		// MARK: - Start Timer

		/// <summary>
		/// Start a timer on the given queue.
		/// </summary>
		/// <param name="queue">The queue to run the timer on.</param>
		/// <param name="timer">The timer to update.</param>
		/// <returns>The timer key, which can be used to stop the timer early.</returns>
		public static CKKey StartTimer(
			CKQueue queue,
			in ICKTimer timer
		)
			=> CKClockController.Shared.queues[queue].StartTimer(timer);

		/// <summary>
		/// Start a timer on the default queue.
		/// </summary>
		/// <param name="timer">The timer to update.</param>
		/// <returns>The timer key, which can be used to stop the timer early.</returns>
		public static CKKey StartTimer(
			in ICKTimer timer
		)
			=> StartTimer(CKQueue.Default, timer);

		// MARK: - Has Timer

		public static bool HasTimer(
			CKQueue queue,
			in CKKey? key
		) => CKClockController.Shared.queues[queue].HasTimer(key);

		public static IEnumerable<bool> HasTimers(
			CKQueue queue,
			IEnumerable<CKKey> keys
		) => keys.Map(key => HasTimer(queue, key));

		// MARK: - Stop Timer

		/// <summary>
		/// Stop a timer on the given queue.
		/// </summary>
		/// <param name="queue">The queue to stop a timer on.</param>
		/// <param name="key">The timer's key.</param>
		/// <returns><see langword="true"/> if a timer was successfully stopped; <see langword="false"/> otherwise.</returns>
		public static bool StopTimer(CKQueue queue, in CKKey? key)
			=> CKClockController.Shared.queues[queue].StopTimer(key);

		/// <summary>
		/// Stop multiple timers on the given queue.
		/// </summary>
		/// <param name="queue">The queue to stop timers on.</param>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped or not.</returns>
		public static IEnumerable<bool> StopTimers(CKQueue queue, in IEnumerable<CKKey> keys)
			=> keys.Map(key => StopTimer(queue, key));

		/// <summary>
		/// Stop multiple timers on the given queue.
		/// </summary>
		/// <param name="queue">The queue to stop timers on.</param>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped or not.</returns>
		public static IEnumerable<bool> StopTimers(CKQueue queue, in IEnumerable<CKKey?> keys)
			=> keys.Map(key => StopTimer(queue, key));

		/// <summary>
		/// Stop timers with the given key on all queues.
		/// </summary>
		/// <param name="key">The timer key to stop on all queues.</param>
		/// <returns><see langword="true"/> if one or more queues successfully stopped a timer with the given key; <see langword="false"/> otherwise.</returns>
		public static bool StopTimer(in CKKey? key) {
			bool result = false;
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				result |= StopTimer(queue, key);
			}
			return result;
		}

		/// <summary>
		/// Stop multiple timers on all queues.
		/// </summary>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped on any queue or not.</returns>
		public static IEnumerable<bool> StopTimers(in IEnumerable<CKKey> keys)
			=> keys.Map(key => StopTimer(key));

		/// <summary>
		/// Stop multiple timers on all queues.
		/// </summary>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped on any queue or not.</returns>
		public static IEnumerable<bool> StopTimers(in IEnumerable<CKKey?> keys)
			=> keys.Map(key => StopTimer(key));

		/// <summary>
		/// Stop all timers on a given queue.
		/// </summary>
		/// <param name="queue">The queue to stop all timers on.</param>
		public static void StopAllTimers(CKQueue queue)
			=> CKClockController.Shared.queues[queue].StopAllTimers();

		/// <summary>
		/// Stop all timers on every queue.
		/// </summary>
		public static void StopAllTimers() {
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				StopAllTimers(queue);
			}
		}

		// MARK: - Start Timer (Convenience)

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
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
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
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.CompletionCallback onComplete = null
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
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			in CKFrameDelayingTimer.CompletionCallback onComplete
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
			int frames,
			in CKFrameDelayingTimer.CompletionCallback onComplete
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
			in Animation animation,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKFiniteAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
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
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
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