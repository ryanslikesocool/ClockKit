using System;

namespace ClockKit {
    public struct CKIndefiniteDelayingTimer : ICKTimer {
        public delegate void CompletionCallback(float localTime);
        public delegate bool CompletionPredicate(float localTime);

        public float StartTime { get; }

        public readonly CompletionCallback onComplete;
        public readonly CompletionPredicate completionPredicate;

        public bool IsComplete { get; private set; }

        public CKIndefiniteDelayingTimer(float startTime, CompletionCallback onComplete, CompletionPredicate completionPredicate) {
            this.StartTime = startTime;
            this.onComplete = onComplete;
            this.completionPredicate = completionPredicate;
            IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;

            IsComplete = completionPredicate(localTime);
            if (IsComplete) {
                onComplete?.Invoke(localTime);
            }
            return IsComplete;
        }
    }
}