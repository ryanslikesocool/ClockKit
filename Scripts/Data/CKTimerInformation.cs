using System;
using UnityEngine;

namespace ClockKit {
    /// <summary>
    /// Information about a timer update.
    /// </summary>
    public readonly struct CKTimerInformation : ICKTimeInformation {
        /// <summary>
        /// The queue that the information comes from.
        /// </summary>
        public readonly CKQueue queue;

        /// <summary>
        /// The queue's current time.
        /// </summary>
        public readonly float time;

        /// <summary>
        /// The amount of time that has passed since this timer was started.
        /// </summary>
        public readonly float localTime;

        public readonly float deltaTime;

        public CKTimerInformation(CKQueue queue, float time, float localTime, float deltaTime) {
            this.queue = queue;
            this.time = time;
            this.localTime = localTime;
            this.deltaTime = deltaTime;
        }

        public CKTimerInformation(CKClockInformation clockInformation, float localTime) : this(clockInformation.queue, clockInformation.time, localTime, clockInformation.deltaTime) { }
    }
}