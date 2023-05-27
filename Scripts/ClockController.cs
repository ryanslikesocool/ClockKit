using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace ClockKit {
    internal sealed class ClockController : AutoSingleton<ClockController> {
        // MARK: - Properties

        internal Dictionary<CKQueue, UpdateQueue> queues = default;

        // MARK: - Lifecycle

        protected override void Awake() {
            base.Awake();

            float time = Time.time;
            queues = new Dictionary<CKQueue, UpdateQueue> {
                { CKQueue.Update, new UpdateQueue(CKQueue.Update, time) },
                { CKQueue.FixedUpdate, new UpdateQueue(CKQueue.FixedUpdate, time) },
                { CKQueue.LateUpdate, new UpdateQueue(CKQueue.LateUpdate, time) },
            };

            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
        }

        protected override void OnApplicationQuit() {
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