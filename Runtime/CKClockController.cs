// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using UnityEngine;

namespace ClockKit {
	[Singleton(Persistent = true, Auto = true)]
	internal sealed partial class CKClockController : MonoBehaviour {
		// MARK: - Properties

		internal Dictionary<CKQueue, CKUpdateQueue> queues = default;

		// MARK: - Lifecycle

		private void Awake() {
			float time = Time.time;
			queues = new Dictionary<CKQueue, CKUpdateQueue> {
				{ CKQueue.Update, new CKUpdateQueue(CKQueue.Update, time) },
				{ CKQueue.FixedUpdate, new CKUpdateQueue(CKQueue.FixedUpdate, time) },
				{ CKQueue.LateUpdate, new CKUpdateQueue(CKQueue.LateUpdate, time) },
			};

			gameObject.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad(gameObject);
		}

		private void OnApplicationQuit() {
			CKClock.RemoveAllUpdateDelegates();
			CKClock.StopAllTimers();

			DeinitializeSingleton();
			// Destroy(gameObject); // called by DeinitializeSingleton();
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