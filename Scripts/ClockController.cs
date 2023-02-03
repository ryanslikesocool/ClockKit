using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace ClockKit {
    internal sealed class ClockController : Singleton<ClockController> {
        // MARK: - Properties

        internal Dictionary<Queue, UpdateQueue> queues = default;

        // MARK: - Lifecycle

        protected override void Awake() {
            base.Awake();

            float time = Time.time;
            queues = new Dictionary<Queue, UpdateQueue> {
                { Queue.Update, new UpdateQueue(Queue.Update, time) },
                { Queue.FixedUpdate, new UpdateQueue(Queue.FixedUpdate, time) },
                { Queue.LateUpdate, new UpdateQueue(Queue.LateUpdate, time) },
            };
        }

        protected override void OnApplicationQuit() {
            base.OnApplicationQuit();
        }

        // MARK: - Update

        private void Update() {
            queues[Queue.Update].Update(Time.time);
        }

        private void FixedUpdate() {
            queues[Queue.FixedUpdate].Update(Time.time);
        }

        private void LateUpdate() {
            queues[Queue.LateUpdate].Update(Time.time);
        }
    }
}