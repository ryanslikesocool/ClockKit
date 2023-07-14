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

		private List<(int, CKKey, CKClock.UpdateCallback)> insertingDelegates;
		private List<CKKey> removingDelegates;

		public bool IsEmpty => TimerCount == 0 && DelegateCount == 0;

		private float time;
		private float previousTime;
		private float deltaTime;
		private uint updateCount;

		private CKKey currentKey;

		public int TimerCount => timers.Count;
		public int DelegateCount => delegates.Count;

		// MARK: - Lifecycle

		public CKUpdateQueue(CKQueue queue, float currentTime) {
			this.Queue = queue;

			this.previousTime = currentTime;
			this.deltaTime = 0;
			this.updateCount = 0;

			this.timers = new Dictionary<CKKey, ICKTimer>();
			this.delegates = new Dictionary<CKKey, CKClock.UpdateCallback>();
			this.updateOrder = new List<(int, CKKey)>();

			this.insertingDelegates = new List<(int, CKKey, CKClock.UpdateCallback)>();
			this.removingDelegates = new List<CKKey>();

			this.currentKey = CKKey.zero;
		}

		~CKUpdateQueue() {
			UnityEngine.Debug.Log($"deinit queue {Queue}");

			this.previousTime = 0;
			this.deltaTime = 0;
			this.updateCount = 0;

			timers.Clear();
			delegates.Clear();
			updateOrder.Clear();
			insertingDelegates.Clear();
			removingDelegates.Clear();

			timers = null;
			delegates = null;
			updateOrder = null;
			insertingDelegates = null;
			removingDelegates = null;
		}

		public void Update(float currentTime) {
			deltaTime = currentTime - previousTime;
			time = currentTime;
			previousTime = currentTime;
			updateCount++;

			if (!IsEmpty) {
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

			FinalizeDelegateInsertion();
			FinalizeDelegateRemoval();
		}

		// MARK: - Utility

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FinalizeDelegateInsertion() {
			if (insertingDelegates.IsEmpty()) {
				return;
			}

			foreach ((int priority, CKKey key, CKClock.UpdateCallback callback) in insertingDelegates) {
				InsertDelegate(priority, key, callback);
			}
			insertingDelegates.Clear();

			ValidateUpdateOrder();

			void InsertDelegate(int priority, CKKey key, CKClock.UpdateCallback callback) {
				delegates.Add(key, callback);
				updateOrder.Add((priority, key));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FinalizeDelegateRemoval() {
			if (removingDelegates.IsEmpty()) {
				return;
			}

			foreach (CKKey key in removingDelegates) {
				RemoveDelegate(key);
			}
			removingDelegates.Clear();

			ValidateUpdateOrder();

			void RemoveDelegate(CKKey key) {
				delegates.Remove(key);
				if (updateOrder.FirstIndex(pair => pair.Item2 == key).TryGetValue(out int index)) {
					updateOrder.RemoveAt(index);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private CKKey RetrieveNextKey() {
			CKKey initialKey = currentKey;
			do {
				currentKey += 1;

				if (initialKey == currentKey) {
					throw new System.ArgumentOutOfRangeException($"This Queue ('{Queue}') is full.  All ${((UInt64)(UInt32.MaxValue)) + 1} keys are occupied.");
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
			insertingDelegates.Add((priority, key, body));
			return key;
		}

		public bool RemoveDelegate(CKKey? key) {
			if (key is not CKKey _key) {
				return false;
			}
			if (!delegates.ContainsKey(_key)) {
				return false;
			}

			removingDelegates.Add(_key);
			return true;
		}

		public void RemoveAllDelegates() {
			foreach (CKKey key in delegates.Keys) {
				RemoveDelegate(key);
			}
		}

		// MARK: - Timers

		public CKKey StartTimer(in ICKTimer timer) {
			CKKey key = RetrieveNextKey();
			timers.Add(key, timer);
			return key;
		}

		public bool StopTimer(in CKKey? key) {
			if (key is not CKKey _key) {
				return false;
			}
			if (!timers.ContainsKey(_key)) {
				return false;
			}

			timers.Remove(_key);
			return true;
		}

		public void StopAllTimers() {
			CKKey[] timerKeys = timers.Keys.ToArray();
			foreach (CKKey key in timerKeys) {
				StopTimer(key);
			}
		}
	}
}