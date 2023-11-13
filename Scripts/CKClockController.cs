using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace ClockKit {
	internal sealed class CKClockController : AutoSingleton<CKClockController> {
		// MARK: - Properties

		internal Dictionary<CKQueue, CKUpdateQueue> queues = default;

		// MARK: - Lifecycle

		protected override void Awake() {
			base.Awake();

			float time = Time.time;
			queues = new Dictionary<CKQueue, CKUpdateQueue> {
				{ CKQueue.Update, new CKUpdateQueue(CKQueue.Update, time) },
				{ CKQueue.FixedUpdate, new CKUpdateQueue(CKQueue.FixedUpdate, time) },
				{ CKQueue.LateUpdate, new CKUpdateQueue(CKQueue.LateUpdate, time) },
			};

			gameObject.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad(gameObject);
		}

		protected override void OnApplicationQuit() {
			CKClock.RemoveAllDelegates();
			CKClock.StopAllTimers();

			base.OnApplicationQuit();
			Destroy(gameObject);
		}

		// MARK: - Update

		private void Update() {
			queues[CKQueue.Update].Update(Time.time);
		}

		private void FixedUpdate() {
			queues[CKQueue.FixedUpdate].Update(Time.time);
		}

		private void LateUpdate() {
			queues[CKQueue.LateUpdate].Update(Time.time);
		}
	}
}