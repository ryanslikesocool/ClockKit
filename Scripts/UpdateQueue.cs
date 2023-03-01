using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace ClockKit {
    internal sealed class UpdateQueue {
        public readonly Queue Queue;

        private Dictionary<UUID, ITimer> timers;
        private Dictionary<UUID, Clock.UpdateCallback> delegates;
        private List<(int, UUID)> updateOrder;

        public bool IsEmpty => timers.Count == 0 && delegates.Count == 0;

        private float time;
        private float previousTime;
        private float deltaTime;
        private uint updateCount;

        // MARK: - Lifecycle

        public UpdateQueue(Queue queue, float currentTime) {
            this.previousTime = currentTime;
            timers = new Dictionary<UUID, ITimer>();
            delegates = new Dictionary<UUID, Clock.UpdateCallback>();
            updateOrder = new List<(int, UUID)>();
        }

        ~UpdateQueue() {
            timers.Clear();
            delegates.Clear();
            updateOrder.Clear();
            timers = null;
            delegates = null;
            updateOrder = null;
        }

        public void Update(float currentTime) {
            deltaTime = currentTime - previousTime;
            time = currentTime;
            previousTime = currentTime;
            updateCount++;

            if (IsEmpty) {
                return;
            }

            ClockInformation information = new ClockInformation(
                queue: Queue,
                time: time,
                deltaTime: deltaTime,
                updateCount: updateCount
            );

            if (updateOrder.Count > 0) {
                foreach ((_, UUID key) in updateOrder) {
                    delegates[key](information);
                }
            }

            if (timers.Count > 0) {
                UUID[] timerKeys = timers.Keys.ToArray();
                foreach (UUID key in timerKeys) {
                    if (timers.ContainsKey(key)) {
                        bool isComplete = timers[key].OnUpdate(information);
                        if (isComplete) {
                            StopTimer(key);
                        }
                    }
                }
            }
        }

        // MARK: - Delegates

        public UUID AddDelegate(in UUID key, int priority, Clock.UpdateCallback body) {
            delegates.Add(key, body);
            updateOrder.Add((priority, key));
            ValidateUpdateOrder();
            return key;
        }

        public bool RemoveDelegate(UUID key) {
            bool result = delegates.ContainsKey(key);
            delegates.Remove(key);
            if (updateOrder.FirstIndex(pair => pair.Item2 == key).TryGetValue(out int index)) {
                updateOrder.RemoveAt(index);
            }
            ValidateUpdateOrder();
            return result;
        }

        private void ValidateUpdateOrder() {
            updateOrder.Sort(new Comparison<(int, UUID)>((i1, i2) => i2.Item1.CompareTo(i1.Item1)));
        }

        // MARK: - Timers

        public UUID StartTimer(in UUID key, ITimer timer) {
            timers.Add(key, timer);
            return key;
        }

        public bool StopTimer(in UUID key)
            => timers.Remove(key);
    }
}