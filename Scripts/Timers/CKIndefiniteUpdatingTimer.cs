using System;

namespace ClockKit {
    public struct CKIndefiniteUpdatingTimer : ICKTimer {
        public delegate void UpdateCallback(in CKTimerInformation information);
        public delegate void CompletionCallback(in CKTimerInformation information);
        public delegate bool CompletionPredicate(in CKTimerInformation information);

        public float StartTime { get; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;
        public readonly CompletionPredicate completionPredicate;

        public bool IsComplete { get; private set; }

        public CKIndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionPredicate completionPredicate) : this(startTime, onUpdate, null, completionPredicate) { }

        public CKIndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionCallback onComplete, CompletionPredicate completionPredicate) {
            this.StartTime = startTime;
            this.onUpdate = onUpdate;
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
            onUpdate?.Invoke(timerInformation);

            IsComplete = completionPredicate(timerInformation);
            if (IsComplete) {
                onComplete?.Invoke(timerInformation);
            }
            return IsComplete;
        }
    }
}