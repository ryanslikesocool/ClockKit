using System;

namespace ClockKit {
    public struct CKFrameDelayingTimer : ICKTimer {
        public delegate void CompletionCallback(in CKTimerInformation timerInformation);

        public float StartTime { get; }
        public int Counter { get; private set; }

        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public CKFrameDelayingTimer(float startTime, int frames, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.Counter = frames;
            this.onComplete = onComplete;
            this.IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            Counter -= 1;


            IsComplete = Counter <= 0;
            if (IsComplete) {
                float localTime = information.time - StartTime;

                CKTimerInformation timerInformation = new CKTimerInformation(information, localTime);
                onComplete?.Invoke(timerInformation);
            }
            return IsComplete;
        }
    }
}