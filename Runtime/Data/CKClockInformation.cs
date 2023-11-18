//using System;
//
//namespace ClockKit {
//    /// <summary>
//    /// Information about a clock update.
//    /// </summary>
//    public readonly struct CKClockInformation : ICKTimeInformation {
//        /// <summary>
//        /// The queue that the information comes from.
//        /// </summary>
//        public readonly CKQueue queue;
//
//        /// <summary>
//        /// The queue's current time.
//        /// </summary>
//        public readonly float time;
//
//        /// <summary>
//        /// The time between the last update and the current update.
//        /// </summary>
//        public readonly float deltaTime;
//
//        /// <summary>
//        /// The number of times the queue has been updated.
//        /// </summary>
//        public readonly uint updateCount;
//
//        internal CKClockInformation(CKQueue queue, float time, float deltaTime, uint updateCount) {
//            this.queue = queue;
//            this.time = time;
//            this.deltaTime = deltaTime;
//            this.updateCount = updateCount;
//        }
//    }
//}