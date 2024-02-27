// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKFiniteUpdatingTimer : ICKFiniteTimer {
		public delegate void UpdateCallback(in CKInstant instant);
		public delegate void CompletionCallback();

		public float StartTime { get; }
		public float Duration { get; }

		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKFiniteUpdatingTimer(float startTime, float seconds, UpdateCallback onUpdate, CompletionCallback onComplete = null) {
			this.StartTime = startTime;
			this.Duration = seconds;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
			this.IsComplete = false;
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

			IsComplete = localTime >= Duration;
			if (IsComplete) {
				onComplete?.Invoke();
			}
			return IsComplete;
		}
	}
}