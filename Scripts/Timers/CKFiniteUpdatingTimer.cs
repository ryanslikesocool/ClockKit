using System;

namespace ClockKit {
    public struct CKFiniteUpdatingTimer : ICKFiniteTimer {
        public delegate void UpdateCallback(in CKTimerInformation information);
        public delegate void CompletionCallback();

        public float StartTime { get; }
        public float Duration { get; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public CKFiniteUpdatingTimer(float startTime, float duration, UpdateCallback onUpdate) : this(startTime, duration, onUpdate, null) { }

        public CKFiniteUpdatingTimer(float startTime, float duration, UpdateCallback onUpdate, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.Duration = duration;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            this.IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;

            CKTimerInformation timerInformation = new CKTimerInformation(information, localTime);
            onUpdate?.Invoke(timerInformation);

            IsComplete = localTime >= Duration;
            if (IsComplete) {
                onComplete?.Invoke();
            }
            return IsComplete;
        }
    }
}