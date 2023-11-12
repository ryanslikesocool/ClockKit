using System;

namespace ClockKit {
	public struct CKFiniteDelayingTimer : ICKFiniteTimer {
		public delegate void CompletionCallback();

		public float StartTime { get; }
		public float Duration { get; }

		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKFiniteDelayingTimer(float startTime, float duration, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.Duration = duration;
			this.onComplete = onComplete;
			this.IsComplete = false;
		}

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			float localTime = instant.localTime - StartTime;

			IsComplete = localTime >= Duration;
			if (IsComplete) {
				onComplete?.Invoke();
			}
			return IsComplete;
		}
	}
}