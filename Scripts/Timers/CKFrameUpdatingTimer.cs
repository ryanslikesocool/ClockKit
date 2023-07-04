using System;

namespace ClockKit {
    public struct CKFrameUpdatingTimer : ICKTimer {
        public delegate void UpdateCallback(in CKTimerInformation timerInformation);
        public delegate void CompletionCallback(in CKTimerInformation timerInformation);

        public float StartTime { get; }
        public int Counter { get; private set; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public CKFrameUpdatingTimer(float startTime, int frames, UpdateCallback onUpdate, CompletionCallback onComplete = null) {
            this.StartTime = startTime;
            this.Counter = frames;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            this.IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            Counter -= 1;

            float localTime = information.time - StartTime;
            CKTimerInformation timerInformation = new CKTimerInformation(information, localTime);

            onUpdate(timerInformation);

            IsComplete = Counter <= 0;
            if (IsComplete) {
                onComplete?.Invoke(timerInformation);
            }
            return IsComplete;
        }
    }
}