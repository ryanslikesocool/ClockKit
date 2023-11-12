using System;

namespace ClockKit {
	public struct CKIndefiniteDelayingTimer : ICKTimer {
		public delegate void CompletionCallback(in CKInstant instant);
		public delegate bool CompletionPredicate(in CKInstant instant);

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

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			float localTime = instant.localTime - StartTime;
			CKInstant timerInstant = new CKInstant(
				instant.queue,
				localTime,
				instant.deltaTime,
				instant.updateCount
			);

			IsComplete = completionPredicate(timerInstant);
			if (IsComplete) {
				onComplete?.Invoke(timerInstant);
			}
			return IsComplete;
		}
	}
}