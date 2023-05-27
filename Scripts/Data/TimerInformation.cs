using System;
using UnityEngine;

namespace ClockKit {
    /// <summary>
    /// Information about a timer update.
    /// </summary>
    public readonly struct TimerInformation : ITimeInformation {
        /// <summary>
        /// The queue that the information comes from.
        /// </summary>
        public readonly Queue queue;

        /// <summary>
        /// The queue's current time.
        /// </summary>
        public readonly float time;

        /// <summary>
        /// The amount of time that has passed since this timer was started.
        /// </summary>
        public readonly float localTime;

        public readonly float deltaTime;

        public TimerInformation(Queue queue, float time, float localTime, float deltaTime) {
            this.queue = queue;
            this.time = time;
            this.localTime = localTime;
            this.deltaTime = deltaTime;
        }

        public TimerInformation(ClockInformation clockInformation, float localTime) : this(clockInformation.queue, clockInformation.time, localTime, clockInformation.deltaTime) { }
    }
}