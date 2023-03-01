using System;

namespace ClockKit {
    public struct SequenceTimer : ITimer {
        public delegate void CompletionCallback();

        public float StartTime { get; private set; }

        public readonly Func<ITimer>[] timerBuilders;

        public ITimer ActiveTimer { get; private set; }
        public int ActiveTimerIndex { get; private set; }

        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public SequenceTimer(float startTime, Func<ITimer>[] timerBuilders, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.timerBuilders = timerBuilders;
            this.onComplete = onComplete;
            this.ActiveTimerIndex = 0;
            this.ActiveTimer = timerBuilders[0]();
            this.IsComplete = false;
        }

        public bool OnUpdate(in ClockInformation information) {
            if (IsComplete) {
                return true;
            }

            bool isCurrentTimerComplete = ActiveTimer.OnUpdate(information);
            if (isCurrentTimerComplete) {
                ActiveTimerIndex++;
                if (ActiveTimerIndex < timerBuilders.Length) {
                    ActiveTimer = timerBuilders[ActiveTimerIndex]();
                }
            }

            IsComplete = isCurrentTimerComplete && ActiveTimerIndex == timerBuilders.Length;
            if (IsComplete) {
                onComplete?.Invoke();
            }
            return IsComplete;
        }
    }
}