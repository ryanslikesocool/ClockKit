// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKIndefiniteUpdatingTimer : ICKTimer {
		public delegate void UpdateCallback(in CKInstant instant);
		public delegate void CompletionCallback(in CKInstant instant);
		public delegate bool CompletionPredicate(in CKInstant instant);

		public float StartTime { get; }

		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;
		public readonly CompletionPredicate completionPredicate;

		public bool IsComplete { get; private set; }

		public CKIndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionPredicate completionPredicate) : this(startTime, onUpdate, null, completionPredicate) { }

		public CKIndefiniteUpdatingTimer(float startTime, UpdateCallback onUpdate, CompletionCallback onComplete, CompletionPredicate completionPredicate) {
			this.StartTime = startTime;
			this.onUpdate = onUpdate;
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
			onUpdate?.Invoke(timerInstant);

			IsComplete = completionPredicate(timerInstant);
			if (IsComplete) {
				onComplete?.Invoke(timerInstant);
			}
			return IsComplete;
		}
	}
}