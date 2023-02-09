using System;

namespace ClockKit {
    /// <summary>
    /// Information about the current update.
    /// </summary>
    public readonly struct Information {
        /// <summary>
        /// The queue that the information comes from.
        /// </summary>
        public readonly Queue queue;

        /// <summary>
        /// The queue's current time.
        /// </summary>
        public readonly float time;

        /// <summary>
        /// The time between the last update and the current update.
        /// </summary>
        public readonly float deltaTime;

        /// <summary>
        /// The number of times the queue has been updated.
        /// </summary>
        public readonly uint updateCount;

        internal Information(Queue queue, float time, float deltaTime, uint updateCount) {
            this.queue = queue;
            this.time = time;
            this.deltaTime = deltaTime;
            this.updateCount = updateCount;
        }
    }
}