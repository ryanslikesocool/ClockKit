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

        // MARK: - Remove Delegate

        /// <summary>
        /// Remove an update delegate from a queue with its key.
        /// </summary>
        /// <param name="queue">The queue the delegate was added to.</param>
        /// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
        /// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
        public static bool RemoveDelegate(CKQueue queue, in CKKey? key)
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

        // MARK: - Start Timer

        public static CKKey StartTimer(
            CKQueue queue,
            in ICKTimer timer
        )
            => CKClockController.Shared.queues[queue].StartTimer(timer);

        public static CKKey StartTimer(
            in ICKTimer timer
        )
            => StartTimer(CKQueue.Default, timer);

        // MARK: - Stop Timer

        public static bool StopTimer(CKQueue queue, in CKKey? key)
            => CKClockController.Shared.queues[queue].StopTimer(key);

        public static IEnumerable<bool> StopTimers(CKQueue queue, in IEnumerable<CKKey> keys)
            => keys.Map(key => StopTimer(key));

        public static bool StopTimer(in CKKey? key) {
            bool result = false;
            foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
                result |= StopTimer(queue, key);
            }
            return result;
        }

        public static IEnumerable<bool> StopTimers(in IEnumerable<CKKey> keys)
            => keys.Map(key => StopTimer(key));

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

        // MARK: Update For

        public static CKKey Update(
            CKQueue queue,
            float duration,
            in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
            in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
        ) {
            ICKTimer timer = new CKFiniteUpdatingTimer(
                startTime: Time.time,
                duration: duration,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Update(
            float duration,
            in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
            in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
        )
            => Update(CKQueue.Default, duration, onUpdate, onComplete);

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