// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKIndefiniteDelayingTimer : ICKTimer {
		public delegate bool CompletionPredicate(in CKInstant instant);
		public delegate void CompletionCallback(in CKInstant instant);
		public delegate bool SimpleCompletionPredicate();
		public delegate void SimpleCompletionCallback();

		public float StartTime { get; }

		public readonly CompletionPredicate completionPredicate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKIndefiniteDelayingTimer(float startTime, CompletionPredicate completionPredicate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.completionPredicate = completionPredicate;
			this.onComplete = onComplete;
			IsComplete = false;
		}

		public CKIndefiniteDelayingTimer(float startTime, CompletionPredicate completionPredicate, SimpleCompletionCallback onComplete) : this(
			startTime,
			completionPredicate,
			(in CKInstant _) => onComplete?.Invoke()
		) { }

		public CKIndefiniteDelayingTimer(float startTime, SimpleCompletionPredicate completionPredicate, CompletionCallback onComplete) : this(
			startTime,
			(in CKInstant _) => completionPredicate(),
			onComplete
		) { }

		public CKIndefiniteDelayingTimer(float startTime, SimpleCompletionPredicate completionPredicate, SimpleCompletionCallback onComplete) : this(
			startTime,
			(in CKInstant _) => completionPredicate(),
			(in CKInstant _) => onComplete?.Invoke()
		) { }

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