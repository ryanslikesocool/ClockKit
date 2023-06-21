using System;

namespace ClockKit {
    public struct CKIndefiniteDelayingTimer : ICKTimer {
        public delegate void CompletionCallback(in CKTimerInformation information);
        public delegate bool CompletionPredicate(in CKTimerInformation information);

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
            CKTimerInformation timerInformation = new CKTimerInformation(information, localTime);

            IsComplete = completionPredicate(timerInformation);
            if (IsComplete) {
                onComplete?.Invoke(timerInformation);
            }
            return IsComplete;
        }
    }
}