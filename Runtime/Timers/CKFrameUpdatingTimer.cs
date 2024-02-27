// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKFrameUpdatingTimer : ICKTimer {
		public delegate void UpdateCallback(in CKInstant instant);
		public delegate void CompletionCallback(in CKInstant instant);
		public delegate void SimpleCompletionCallback();

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

		public CKFrameUpdatingTimer(float startTime, int frames, UpdateCallback onUpdate, SimpleCompletionCallback onComplete)
			: this(startTime, frames, onUpdate, (in CKInstant _) => onComplete()) { }

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			Counter -= 1;

			float localTime = instant.localTime - StartTime;

			CKInstant timerInstant = new CKInstant(
				instant.queue,
				localTime,
				instant.deltaTime,
				instant.updateCount
			);

			onUpdate(timerInstant);

			IsComplete = Counter <= 0;
			if (IsComplete) {
				onComplete?.Invoke(timerInstant);
			}
			return IsComplete;
		}
	}
}