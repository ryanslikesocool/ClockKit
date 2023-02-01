using System;

namespace ClockKit {
    public struct FixedDurationUpdatingTimer : IFixedDurationTimer {
        public delegate void UpdateCallback(float localTime);
        public delegate void CompletionCallback();

        public float StartTime { get; }
        public float Duration { get; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public FixedDurationUpdatingTimer(float startTime, float duration, UpdateCallback onUpdate) : this(startTime, duration, onUpdate, null) { }

        public FixedDurationUpdatingTimer(float startTime, float duration, UpdateCallback onUpdate, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.Duration = duration;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            this.IsComplete = false;
        }

        public bool Update(in Information information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;
            onUpdate(localTime);

            IsComplete = localTime >= Duration;
            if (IsComplete) {
                onComplete?.Invoke();
            }
            return IsComplete;
        }
    }
}