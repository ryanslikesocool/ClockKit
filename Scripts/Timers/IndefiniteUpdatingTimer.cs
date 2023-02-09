using System;

namespace ClockKit {
    public struct IndefiniteUpdatingTimer : ITimer {
        public delegate void UpdateCallback(float localTime);
        public delegate void CompletionCallback(float localTime);
        public delegate bool CompletionPredicate(float localTime);

        public float StartTime { get; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;
        public readonly CompletionPredicate completionPredicate;

        public bool IsComplete { get; private set; }

        public IndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionPredicate completionPredicate) : this(startTime, onUpdate, null, completionPredicate) { }

        public IndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionCallback onComplete, CompletionPredicate completionPredicate) {
            this.StartTime = startTime;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            this.completionPredicate = completionPredicate;
            IsComplete = false;
        }

        public bool Update(in Information information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;
            onUpdate?.Invoke(localTime);

            IsComplete = completionPredicate(localTime);
            if (IsComplete) {
                onComplete?.Invoke(localTime);
            }
            return IsComplete;
        }
    }
}