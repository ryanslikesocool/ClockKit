// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKIndefiniteUpdatingTimer : ICKTimer {
		public delegate bool CompletionPredicate(in CKInstant instant);
		public delegate bool SimpleCompletionPredicate();

		public delegate void UpdateCallback(in CKInstant instant);

		public delegate void CompletionCallback(in CKInstant instant);
		public delegate void SimpleCompletionCallback();

		public float StartTime { get; }

		public readonly CompletionPredicate completionPredicate;
		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKIndefiniteUpdatingTimer(float startTime, CompletionPredicate completionPredicate, UpdateCallback onUpdate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.completionPredicate = completionPredicate;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
			IsComplete = false;
		}

		public CKIndefiniteUpdatingTimer(float startTime, SimpleCompletionPredicate completionPredicate, UpdateCallback onUpdate, CompletionCallback onComplete) : this(
			startTime: startTime,
			completionPredicate: (in CKInstant _) => completionPredicate(),
			onUpdate: onUpdate,
			onComplete: onComplete
		) { }

		public CKIndefiniteUpdatingTimer(float startTime, CompletionPredicate completionPredicate, UpdateCallback onUpdate, SimpleCompletionCallback onComplete) : this(
			startTime: startTime,
			completionPredicate: completionPredicate,
			onUpdate: onUpdate,
			onComplete: (in CKInstant _) => onComplete?.Invoke()
		) { }

		public CKIndefiniteUpdatingTimer(float startTime, SimpleCompletionPredicate completionPredicate, UpdateCallback onUpdate, SimpleCompletionCallback onComplete) : this(
			startTime: startTime,
			completionPredicate: (in CKInstant _) => completionPredicate(),
			onUpdate: onUpdate,
			onComplete: (in CKInstant _) => onComplete?.Invoke()
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
			onUpdate?.Invoke(timerInstant);

			IsComplete = completionPredicate(timerInstant);
			if (IsComplete) {
				onComplete?.Invoke(timerInstant);
			}
			return IsComplete;
		}
	}
}