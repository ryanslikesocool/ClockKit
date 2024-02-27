// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKFinitePausableUpdatingTimer : ICKFiniteTimer {
		public delegate bool PauseCheck();
		public delegate void UpdateCallback(in CKInstant instant);
		public delegate void CompletionCallback();

		public float StartTime { get; private set; }
		public float Duration { get; }

		public readonly PauseCheck isPaused;
		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKFinitePausableUpdatingTimer(float startTime, float duration, PauseCheck isPaused, UpdateCallback onUpdate) : this(startTime, duration, isPaused, onUpdate, null) { }

		public CKFinitePausableUpdatingTimer(float startTime, float duration, PauseCheck isPaused, UpdateCallback onUpdate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.Duration = duration;
			this.isPaused = isPaused;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
			this.IsComplete = false;
		}

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			if (isPaused()) {
				StartTime += instant.deltaTime;
				return false;
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