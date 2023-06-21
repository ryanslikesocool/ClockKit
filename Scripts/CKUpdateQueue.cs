using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Foundation;

namespace ClockKit {
    internal sealed class CKUpdateQueue {
        public readonly CKQueue Queue;

        private Dictionary<CKKey, ICKTimer> timers;
        private Dictionary<CKKey, CKClock.UpdateCallback> delegates;
        private List<(int, CKKey)> updateOrder;

        public bool IsEmpty => timers.Count == 0 && delegates.Count == 0;

        private float time;
        private float previousTime;
        private float deltaTime;
        private uint updateCount;

        private CKKey currentKey;

        // MARK: - Lifecycle

        public CKUpdateQueue(CKQueue queue, float currentTime) {
            this.Queue = queue;

            this.previousTime = currentTime;
            this.deltaTime = 0;
            this.updateCount = 0;

            this.timers = new Dictionary<CKKey, ICKTimer>();
            this.delegates = new Dictionary<CKKey, CKClock.UpdateCallback>();
            this.updateOrder = new List<(int, CKKey)>();

            this.currentKey = CKKey.zero;
        }

        ~CKUpdateQueue() {
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

            CKClockInformation information = new CKClockInformation(
                queue: Queue,
                time: time,
                deltaTime: deltaTime,
                updateCount: updateCount
            );

            if (updateOrder.Count > 0) {
                foreach ((_, CKKey key) in updateOrder) {
                    delegates[key](information);
                }
            }

            if (timers.Count > 0) {
                CKKey[] timerKeys = timers.Keys.ToArray();
                foreach (CKKey key in timerKeys) {
                    if (timers.ContainsKey(key)) {
                        bool isComplete = timers[key].OnUpdate(information);
                        if (isComplete) {
                            StopTimer(key);
                        }
                    }
                }
            }
        }

        // MARK: - Utility

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private CKKey RetrieveNextKey() {
            CKKey initialKey = currentKey;
            do {
                currentKey += 1;

                if (initialKey == currentKey) {
                    throw new System.ArgumentOutOfRangeException($"This Queue is full.  All ${((UInt64)(UInt32.MaxValue)) + 1} keys are occupied.");
                }
            } while (delegates.ContainsKey(currentKey) || timers.ContainsKey(currentKey));
            return currentKey;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateUpdateOrder() {
            updateOrder.Sort(new Comparison<(int, CKKey)>((i1, i2) => i2.Item1.CompareTo(i1.Item1)));
        }

        // MARK: - Delegates

        public CKKey AddDelegate(int priority, in CKClock.UpdateCallback body) {
            CKKey key = RetrieveNextKey();
            delegates.Add(key, body);
            updateOrder.Add((priority, key));
            ValidateUpdateOrder();
            return key;
        }

        public bool RemoveDelegate(CKKey key) {
            bool result = delegates.ContainsKey(key);
            delegates.Remove(key);
            if (updateOrder.FirstIndex(pair => pair.Item2 == key).TryGetValue(out int index)) {
                updateOrder.RemoveAt(index);
            }
            ValidateUpdateOrder();
            return result;
        }

        // MARK: - Timers

        public CKKey StartTimer(in ICKTimer timer) {
            CKKey key = RetrieveNextKey();
            timers.Add(key, timer);
            return key;
        }

        public bool StopTimer(in CKKey? key) {
            if (key is CKKey _key) {
                return timers.Remove(_key);
            }
            return false;
        }
    }
}