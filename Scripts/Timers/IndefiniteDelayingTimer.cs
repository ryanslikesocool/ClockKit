using System;

namespace ClockKit {
    public struct IndefiniteDelayingTimer : ITimer {
        public delegate void CompletionCallback(float localTime);
        public delegate bool CompletionPredicate(float localTime);

        public float StartTime { get; }

        public readonly CompletionCallback onComplete;
        public readonly CompletionPredicate completionPredicate;

        public bool IsComplete { get; private set; }

        public IndefiniteDelayingTimer(float startTime, CompletionCallback onComplete, CompletionPredicate completionPredicate) {
            this.StartTime = startTime;
            this.onComplete = onComplete;
            this.completionPredicate = completionPredicate;
            IsComplete = false;
        }

        public bool Update(in Information information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;

            IsComplete = completionPredicate(localTime);
            if (IsComplete) {
                onComplete(localTime);
            }
            return IsComplete;
        }
    }
}