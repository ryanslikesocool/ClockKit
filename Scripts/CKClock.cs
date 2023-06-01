using System;
using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace ClockKit {
    public static partial class CKClock {
        public delegate void UpdateCallback(in ClockInformation information);

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
            => ClockController.Shared.queues[queue].AddDelegate(priority, callback);

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
        /// <param name="updatable">The object to add to the queue.  Its <see cref="IUpdatable.OnUpdate(in ClockInformation)"/> function will be called every update cycle.</param>
        /// <returns>The delegate key.</returns>
        public static CKKey AddDelegate(
            CKQueue queue,
            int priority,
            in IUpdatable updatable
        )
            => AddDelegate(queue, priority, updatable.OnUpdate);

        /// <summary>
        /// Add an update delegate.
        /// </summary>
        /// <param name="queue">The queue to update on.</param>
        /// <param name="updatable">The object to add to the queue.  Its <see cref="IUpdatable.OnUpdate(in ClockInformation)"/> function will be called every update cycle.</param>
        /// <returns>The delegate key.</returns>
        public static CKKey AddDelegate(
            CKQueue queue,
            in IUpdatable updatable
        )
            => AddDelegate(queue, 0, updatable.OnUpdate);

        /// <summary>
        /// Add an update delegate.
        /// </summary>
        /// <param name="priority">The delegate priority.  Higher values are updated first.</param>
        /// <param name="updatable">The object to add to the queue.  Its <see cref="IUpdatable.OnUpdate(in ClockInformation)"/> function will be called every update cycle.</param>
        /// <returns>The delegate key.</returns>
        public static CKKey AddDelegate(
            int priority,
            in IUpdatable updatable
        )
            => AddDelegate(CKQueue.Default, priority, updatable.OnUpdate);

        /// <summary>
        /// Add an update delegate.
        /// </summary>
        /// <param name="updatable">The object to add to the queue.  Its <see cref="IUpdatable.OnUpdate(in ClockInformation)"/> function will be called every update cycle.</param>
        /// <returns>The delegate key.</returns>
        public static CKKey AddDelegate(
            in IUpdatable updatable
        )
            => AddDelegate(CKQueue.Default, 0, updatable.OnUpdate);

        // MARK: - Remove Delegate

        /// <summary>
        /// Remove an update delegate from a queue with its key.
        /// </summary>
        /// <param name="queue">The queue the delegate was added to.</param>
        /// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
        /// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
        public static bool RemoveDelegate(CKQueue queue, in CKKey key)
            => ClockController.Shared.queues[queue].RemoveDelegate(key);

        /// <summary>
        /// Remove an update delegate with its key.  All queues will attempt to remove the delegate.
        /// </summary>
        /// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
        /// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
        public static bool RemoveDelegate(in CKKey key) {
            bool result = false;
            foreach (CKQueue queue in ClockController.Shared.queues.Keys) {
                result |= RemoveDelegate(queue, key);
            }
            return result;
        }

        // MARK: - Start Timer

        public static CKKey StartTimer(
            CKQueue queue,
            in ITimer timer
        )
            => ClockController.Shared.queues[queue].StartTimer(timer);

        public static CKKey StartTimer(
            in ITimer timer
        )
            => StartTimer(CKQueue.Default, timer);

        // MARK: - Stop Timer

        public static bool StopTimer(CKQueue queue, in CKKey key)
            => ClockController.Shared.queues[queue].StopTimer(key);

        public static IEnumerable<bool> StopTimers(CKQueue queue, in CKKey[] keys)
            => keys.Map(key => StopTimer(key));

        public static bool StopTimer(in CKKey key) {
            bool result = false;
            foreach (CKQueue queue in ClockController.Shared.queues.Keys) {
                result |= StopTimer(queue, key);
            }
            return result;
        }

        public static IEnumerable<bool> StopTimers(in CKKey[] keys)
            => keys.Map(key => StopTimer(key));

        // MARK: - Start Timer (Convenience)

        // MARK: Update Until

        public static CKKey Update(
            CKQueue queue,
            in IndefiniteUpdatingTimer.CompletionPredicate until,
            in IndefiniteUpdatingTimer.UpdateCallback onUpdate,
            in IndefiniteUpdatingTimer.CompletionCallback onComplete = null
        ) {
            ITimer timer = new IndefiniteUpdatingTimer(
                startTime: Time.time,
                onUpdate: onUpdate,
                onComplete: onComplete,
                completionPredicate: until
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Update(
            in IndefiniteUpdatingTimer.CompletionPredicate until,
            in IndefiniteUpdatingTimer.UpdateCallback onUpdate,
            in IndefiniteUpdatingTimer.CompletionCallback onComplete = null
        )
            => Update(CKQueue.Default, until, onUpdate, onComplete);

        // MARK: Update For

        public static CKKey Update(
            CKQueue queue,
            float duration,
            in FiniteUpdatingTimer.UpdateCallback onUpdate,
            in FiniteUpdatingTimer.CompletionCallback onComplete = null
        ) {
            ITimer timer = new FiniteUpdatingTimer(
                startTime: Time.time,
                duration: duration,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Update(
            float duration,
            in FiniteUpdatingTimer.UpdateCallback onUpdate,
            in FiniteUpdatingTimer.CompletionCallback onComplete = null
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
            in FrameDelayingTimer.CompletionCallback onComplete
        )
            => Delay(queue, frames: 1, onComplete);

        /// <summary>
        /// Delays a function call for a single update cycle (normally a single frame).
        /// </summary>
        /// <param name="onComplete">The function to call when the timer is complete.</param>
        /// <returns>The timer key.</returns>
        public static CKKey Delay(
            in FrameDelayingTimer.CompletionCallback onComplete
        )
            => Delay(CKQueue.Default, frames: 1, onComplete);

        // MARK: Delay Frames

        public static CKKey Delay(
            CKQueue queue,
            int frames,
            in FrameDelayingTimer.CompletionCallback onComplete
        ) {
            ITimer timer = new FrameDelayingTimer(
                startTime: Time.time,
                frames: frames,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Delay(
            int frames,
            in FrameDelayingTimer.CompletionCallback onComplete
        )
            => Delay(CKQueue.Default, frames, onComplete);

        // MARK: Delay Until

        public static CKKey Delay(
            CKQueue queue,
            in IndefiniteDelayingTimer.CompletionPredicate until,
            in IndefiniteDelayingTimer.CompletionCallback onComplete = null
        ) {
            ITimer timer = new IndefiniteDelayingTimer(
                startTime: Time.time,
                onComplete: onComplete,
                completionPredicate: until
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Delay(
            in IndefiniteDelayingTimer.CompletionPredicate until,
            in IndefiniteDelayingTimer.CompletionCallback onComplete = null
        )
            => Delay(CKQueue.Default, until, onComplete);

        // MARK: Delay For

        public static CKKey Delay(
            CKQueue queue,
            float duration,
            in FiniteDelayingTimer.CompletionCallback onComplete
        ) {
            ITimer timer = new FiniteDelayingTimer(
                startTime: Time.time,
                duration: duration,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Delay(
            float duration,
            in FiniteDelayingTimer.CompletionCallback onComplete
        )
            => Delay(CKQueue.Default, duration, onComplete);

        // MARK: Animate

        public static CKKey Animate<Value, Animation>(
            CKQueue queue,
            in Animation animation,
            in FiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
        ) where Animation : IFiniteAnimation<Value> {
            ITimer timer = new FiniteAnimationUpdatingTimer<Value, Animation>(
                startTime: Time.time,
                animation: animation,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Animate<Value, Animation>(
            in Animation animation,
            in FiniteAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
            in FiniteAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
        ) where Animation : IFiniteAnimation<Value>
            => Animate(CKQueue.Default, animation, onUpdate, onComplete);

        public static CKKey Animate<Value, Animation>(
            CKQueue queue,
            in Animation animation,
            in CompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
            in CompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
        ) where Animation : ICompletableAnimation<Value> {
            ITimer timer = new CompletableAnimationUpdatingTimer<Value, Animation>(
                startTime: Time.time,
                animation: animation,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Animate<Value, Animation>(
            in Animation animation,
            in CompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
            in CompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
        ) where Animation : ICompletableAnimation<Value>
            => Animate(CKQueue.Default, animation, onUpdate, onComplete);

        // MARK: Sequence

        public static CKKey Sequence(
            CKQueue queue,
            in Func<ITimer>[] timerBuilders,
            in SequenceTimer.CompletionCallback onComplete = null
        ) {
            ITimer timer = new SequenceTimer(
                startTime: Time.time,
                timerBuilders: timerBuilders,
                onComplete: onComplete
            );
            return StartTimer(queue, timer);
        }

        public static CKKey Sequence(
            in Func<ITimer>[] timerBuilders,
            in SequenceTimer.CompletionCallback onComplete = null
        )
            => Sequence(CKQueue.Default, timerBuilders, onComplete);
    }
}