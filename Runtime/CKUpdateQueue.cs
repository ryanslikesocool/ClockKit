// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Collections.Generic;
using Foundation;

namespace ClockKit {
	internal sealed class CKUpdateQueue {
		public readonly CKQueue Queue;

		private Dictionary<CKKey, ICKTimer> timers;

		private Dictionary<CKKey, ICKUpdateDelegate> updateDelegates;
		private List<(int, CKKey)> updateDelegateOrder;

		private List<(int, CKKey, ICKUpdateDelegate)> insertingUpdateDelegates;
		private List<CKKey> removingUpdateDelegates;

		private float localTime;
		private float previousTime;
		private float deltaTime;
		private uint updateCount;

		private CKKey currentTimerKey;
		private CKKey currentUpdateDelegateKey;

		public bool IsEmpty => TimerCount == 0 && UpdateDelegateCount == 0;

		public int TimerCount => timers.Count;
		public int UpdateDelegateCount => updateDelegates.Count;

		// MARK: - Lifecycle

		public CKUpdateQueue(CKQueue queue, float currentTime) {
			this.Queue = queue;

			this.previousTime = currentTime;
			this.deltaTime = 0;
			this.updateCount = 0;

			this.timers = new Dictionary<CKKey, ICKTimer>();
			this.updateDelegates = new Dictionary<CKKey, ICKUpdateDelegate>();
			this.updateDelegateOrder = new List<(int, CKKey)>();

			this.insertingUpdateDelegates = new List<(int, CKKey, ICKUpdateDelegate)>();
			this.removingUpdateDelegates = new List<CKKey>();

			this.currentTimerKey = new CKKey(queue, CKKeyAssociation.Timer, 0);
			this.currentUpdateDelegateKey = new CKKey(queue, CKKeyAssociation.UpdateDelegate, 0);
		}

		~CKUpdateQueue() {
			this.previousTime = 0;
			this.deltaTime = 0;
			this.updateCount = 0;

			timers.Clear();
			updateDelegates.Clear();
			updateDelegateOrder.Clear();
			insertingUpdateDelegates.Clear();
			removingUpdateDelegates.Clear();

			timers = null;
			updateDelegates = null;
			updateDelegateOrder = null;
			insertingUpdateDelegates = null;
			removingUpdateDelegates = null;
		}

		public void Update(float currentTime) {
			deltaTime = currentTime - previousTime;
			localTime = currentTime;
			previousTime = currentTime;
			updateCount++;

			if (!IsEmpty) {
				CKInstant instant = new CKInstant(
					queue: Queue,
					localTime: localTime,
					deltaTime: deltaTime,
					updateCount: updateCount
				);

				if (updateDelegateOrder.Count > 0) {
					foreach ((_, CKKey key) in updateDelegateOrder) {
						updateDelegates[key].OnUpdate(instant);
					}
				}

				if (timers.Count > 0) {
					CKKey[] timerKeys = timers.Keys.ToArray();
					foreach (CKKey key in timerKeys) {
						if (timers.ContainsKey(key)) {
							bool isComplete = timers[key].OnUpdate(instant);
							if (isComplete) {
								StopTimer(key);
							}
						}
					}
				}
			}

			FinalizeUpdateDelegateInsertion();
			FinalizeUpdateDelegateRemoval();
		}

		// MARK: - Utility

		private void FinalizeUpdateDelegateInsertion() {
			if (insertingUpdateDelegates.IsEmpty()) {
				return;
			}

			foreach ((int priority, CKKey key, ICKUpdateDelegate callback) in insertingUpdateDelegates) {
				InsertUpdateDelegate(priority, key, callback);
			}
			insertingUpdateDelegates.Clear();

			ValidateUpdateDelegateOrder();

			void InsertUpdateDelegate(int priority, CKKey key, ICKUpdateDelegate callback) {
				updateDelegates.Add(key, callback);
				updateDelegateOrder.Add((priority, key));
			}
		}

		private void FinalizeUpdateDelegateRemoval() {
			if (removingUpdateDelegates.IsEmpty()) {
				return;
			}

			foreach (CKKey key in removingUpdateDelegates) {
				RemoveUpdateDelegate(key);
			}
			removingUpdateDelegates.Clear();

			ValidateUpdateDelegateOrder();

			void RemoveUpdateDelegate(CKKey key) {
				updateDelegates.Remove(key);
				if (updateDelegateOrder.FirstIndex(pair => pair.Item2 == key).TryGetValue(out int index)) {
					updateDelegateOrder.RemoveAt(index);
				}
			}
		}

		private void ValidateUpdateDelegateOrder() {
			updateDelegateOrder.Sort(new Comparison<(int, CKKey)>((i1, i2) => i2.Item1.CompareTo(i1.Item1)));
		}

		// MARK: - Delegates

		public CKKey AddUpdateDelegate(int priority, in ICKUpdateDelegate updateDelegate) {
			CKKey key = RetrieveNextUpdateDelegateKey();
			insertingUpdateDelegates.Add((priority, key, updateDelegate));
			return key;
		}

		public bool HasUpdateDelegate(in CKKey key) {
			if (!IsKeyValid(key, CKKeyAssociation.UpdateDelegate)) {
				return false;
			}
			return updateDelegates.ContainsKey(key);
		}

		public bool RemoveUpdateDelegate(in CKKey key) {
			if (!HasUpdateDelegate(key)) {
				return false;
			}

			removingUpdateDelegates.Add(key);
			return true;
		}

		public void RemoveAllUpdateDelegates() {
			foreach (CKKey key in updateDelegates.Keys) {
				RemoveUpdateDelegate(key);
			}
		}

		// MARK: - Timers

		public CKKey StartTimer(in ICKTimer timer) {
			CKKey key = RetrieveNextTimerKey();
			timers.Add(key, timer);
			return key;
		}

		public bool HasTimer(in CKKey key) {
			if (!IsKeyValid(key, CKKeyAssociation.Timer)) {
				return false;
			}
			return timers.ContainsKey(key);
		}

		public bool StopTimer(in CKKey key) {
			if (!HasTimer(key)) {
				return false;
			}

			timers.Remove(key);
			return true;
		}

		public void StopAllTimers() {
			foreach (CKKey key in timers.Keys) {
				StopTimer(key);
			}
		}

		// MARK: - Keys

		/// <summary>
		/// Is the given key supported on this queue, and does it match the given association?
		/// </summary>
		private bool IsKeyValid(in CKKey key, CKKeyAssociation association)
			=> key.queue == Queue && key.association == association;

		private CKKey RetrieveNextTimerKey() {
			CKKey resultKey = currentTimerKey;

			do {
				resultKey += 1;

				if (currentTimerKey == resultKey) {
					throw new ArgumentOutOfRangeException($"This Queue ('{Queue}') cannot accommodate any more timers.  All ${((ulong)uint.MaxValue) + 1} timer keys are occupied.");
				}
			} while (timers.ContainsKey(resultKey));

			currentTimerKey = resultKey;
			return resultKey;
		}

		private CKKey RetrieveNextUpdateDelegateKey() {
			CKKey resultKey = currentUpdateDelegateKey;

			do {
				resultKey += 1;

				if (currentUpdateDelegateKey == resultKey) {
					throw new ArgumentOutOfRangeException($"This Queue ('{Queue}') cannot accommodate any more update delegates.  All ${((ulong)uint.MaxValue) + 1} update delegate keys are occupied.");
				}
			} while (updateDelegates.ContainsKey(resultKey));

			currentUpdateDelegateKey = resultKey;
			return resultKey;
		}
	}
}