using Foundation;
using UnityEngine;

namespace ClockKit {
    public static partial class Clock {
        public delegate void UpdateCallback(in Information information);

        // MARK: - Add Delegate

        public static UUID AddDelegate(Queue queue, in UUID key, int priority, UpdateCallback callback)
            => ClockController.Shared.queues[queue].AddDelegate(key, priority, callback);

        public static UUID AddDelegate(in UUID key, int priority, UpdateCallback callback)
            => AddDelegate(Queue.Default, key, priority, callback);

        public static UUID AddDelegate(Queue queue, int priority, UpdateCallback callback)
            => AddDelegate(queue, UUID.Create(), priority, callback);

        public static UUID AddDelegate(Queue queue, in UUID key, UpdateCallback callback)
            => AddDelegate(queue, key, 0, callback);

        public static UUID AddDelegate(Queue queue, UpdateCallback callback)
            => AddDelegate(queue, UUID.Create(), 0, callback);

        public static UUID AddDelegate(in UUID key, UpdateCallback callback)
            => AddDelegate(Queue.Default, key, 0, callback);

        public static UUID AddDelegate(int priority, UpdateCallback callback)
            => AddDelegate(Queue.Default, UUID.Create(), priority, callback);

        public static UUID AddDelegate(UpdateCallback callback)
            => AddDelegate(Queue.Default, UUID.Create(), 0, callback);

        // MARK: - Remove Delegate

        public static bool RemoveDelegate(Queue queue, UUID? key) {
            if (!key.TryGetValue(out UUID _key)) {
                return false;
            }
            return ClockController.Shared.queues[queue].RemoveDelegate(_key);
        }

        public static bool RemoveDelegate(UUID? key) {
            if (!key.TryGetValue(out UUID _key)) {
                return false;
            }

            bool result = false;
            foreach (Queue queue in ClockController.Shared.queues.Keys) {
                result |= RemoveDelegate(queue, _key);
            }
            return result;
        }

        // MARK: - Start Timer

        public static UUID StartTimer(Queue queue, in UUID key, ITimer timer)
            => ClockController.Shared.queues[queue].StartTimer(key, timer);

        public static UUID StartTimer(in UUID key, ITimer timer)
            => StartTimer(Queue.Default, key, timer);

        public static UUID StartTimer(Queue queue, ITimer timer)
            => StartTimer(queue, UUID.Create(), timer);

        public static UUID StartTimer(ITimer timer)
            => StartTimer(Queue.Default, UUID.Create(), timer);

        // MARK: - Stop Timer

        public static bool StopTimer(Queue queue, in UUID? key) {
            if (!key.TryGetValue(out UUID _key)) {
                return false;
            }
            return ClockController.Shared.queues[queue].StopTimer(_key);
        }

        public static bool StopTimer(in UUID? key) {
            if (!key.TryGetValue(out UUID _key)) {
                return false;
            }

            bool result = false;
            foreach (Queue queue in ClockController.Shared.queues.Keys) {
                result |= StopTimer(queue, _key);
            }
            return result;
        }

        // MARK: - Start Timer (Convenience)

        // MARK: Update Until
        public static UUID Update(Queue queue, in UUID key, IndefiniteUpdatingTimer.CompletionPredicate until, IndefiniteUpdatingTimer.UpdateCallback onUpdate, IndefiniteUpdatingTimer.CompletionCallback onComplete = null) {
            ITimer timer = new IndefiniteUpdatingTimer(
                startTime: Time.time,
                onUpdate: onUpdate,
                onComplete: onComplete,
                completionPredicate: until
            );
            return StartTimer(queue, key, timer);
        }

        public static UUID Update(in UUID key, IndefiniteUpdatingTimer.CompletionPredicate until, IndefiniteUpdatingTimer.UpdateCallback onUpdate, IndefiniteUpdatingTimer.CompletionCallback onComplete = null)
            => Update(Queue.Default, key, until, onUpdate, onComplete);

        public static UUID Update(Queue queue, IndefiniteUpdatingTimer.CompletionPredicate until, IndefiniteUpdatingTimer.UpdateCallback onUpdate, IndefiniteUpdatingTimer.CompletionCallback onComplete = null)
            => Update(queue, UUID.Create(), until, onUpdate, onComplete);

        public static UUID Update(IndefiniteUpdatingTimer.CompletionPredicate until, IndefiniteUpdatingTimer.UpdateCallback onUpdate, IndefiniteUpdatingTimer.CompletionCallback onComplete = null)
            => Update(Queue.Default, UUID.Create(), until, onUpdate, onComplete);

        // MARK: Update For
        public static UUID Update(Queue queue, in UUID key, float duration, FixedDurationUpdatingTimer.UpdateCallback onUpdate, FixedDurationUpdatingTimer.CompletionCallback onComplete = null) {
            ITimer timer = new FixedDurationUpdatingTimer(
                startTime: Time.time,
                duration: duration,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, key, timer);
        }

        public static UUID Update(in UUID key, float duration, FixedDurationUpdatingTimer.UpdateCallback onUpdate, FixedDurationUpdatingTimer.CompletionCallback onComplete = null)
            => Update(Queue.Default, key, duration, onUpdate, onComplete);

        public static UUID Update(Queue queue, float duration, FixedDurationUpdatingTimer.UpdateCallback onUpdate, FixedDurationUpdatingTimer.CompletionCallback onComplete = null)
            => Update(queue, UUID.Create(), duration, onUpdate, onComplete);

        public static UUID Update(float duration, FixedDurationUpdatingTimer.UpdateCallback onUpdate, FixedDurationUpdatingTimer.CompletionCallback onComplete = null)
            => Update(Queue.Default, UUID.Create(), duration, onUpdate, onComplete);

        // MARK: Delay Until
        public static UUID Delay(Queue queue, in UUID key, IndefiniteDelayingTimer.CompletionPredicate until, IndefiniteDelayingTimer.CompletionCallback onComplete = null) {
            ITimer timer = new IndefiniteDelayingTimer(
                startTime: Time.time,
                onComplete: onComplete,
                completionPredicate: until
            );
            return StartTimer(queue, key, timer);
        }

        public static UUID Delay(in UUID key, IndefiniteDelayingTimer.CompletionPredicate until, IndefiniteDelayingTimer.CompletionCallback onComplete = null)
            => Delay(Queue.Default, key, until, onComplete);

        public static UUID Delay(Queue queue, IndefiniteDelayingTimer.CompletionPredicate until, IndefiniteDelayingTimer.CompletionCallback onComplete = null)
            => Delay(queue, UUID.Create(), until, onComplete);

        public static UUID Delay(IndefiniteDelayingTimer.CompletionPredicate until, IndefiniteDelayingTimer.CompletionCallback onComplete = null)
            => Delay(Queue.Default, UUID.Create(), until, onComplete);

        // MARK: Delay For
        public static UUID Delay(Queue queue, in UUID key, float duration, FixedDurationDelayingTimer.CompletionCallback onComplete) {
            ITimer timer = new FixedDurationDelayingTimer(
                startTime: Time.time,
                duration: duration,
                onComplete: onComplete
            );
            return StartTimer(queue, key, timer);
        }

        public static UUID Delay(in UUID key, float duration, FixedDurationDelayingTimer.CompletionCallback onComplete)
            => Delay(Queue.Default, key, duration, onComplete);

        public static UUID Delay(Queue queue, float duration, FixedDurationDelayingTimer.CompletionCallback onComplete)
            => Delay(queue, UUID.Create(), duration, onComplete);

        public static UUID Delay(float duration, FixedDurationDelayingTimer.CompletionCallback onComplete)
            => Delay(Queue.Default, UUID.Create(), duration, onComplete);

        // MARK: Animate
        public static UUID Animate<Value, Animation>(Queue queue, in UUID key, Animation animation, AnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate, AnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null) where Animation : IFixedDurationAnimation<Value> {
            ITimer timer = new AnimationUpdatingTimer<Value, Animation>(
                startTime: Time.time,
                animation: animation,
                onUpdate: onUpdate,
                onComplete: onComplete
            );
            return StartTimer(queue, key, timer);
        }

        public static UUID Animate<Value, Animation>(in UUID key, Animation animation, AnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate, AnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null) where Animation : IFixedDurationAnimation<Value>
            => Animate(Queue.Default, key, animation, onUpdate, onComplete);

        public static UUID Animate<Value, Animation>(Queue queue, Animation animation, AnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate, AnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null) where Animation : IFixedDurationAnimation<Value>
            => Animate(queue, UUID.Create(), animation, onUpdate, onComplete);

        public static UUID Animate<Value, Animation>(Animation animation, AnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate, AnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null) where Animation : IFixedDurationAnimation<Value>
            => Animate(Queue.Default, UUID.Create(), animation, onUpdate, onComplete);
    }
}