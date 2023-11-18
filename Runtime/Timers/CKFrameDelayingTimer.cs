using System;

namespace ClockKit {
	public struct CKFrameDelayingTimer : ICKTimer {
		public delegate void CompletionCallback(in CKInstant instant);
		public delegate void SimpleCompletionCallback();

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

		public CKFrameDelayingTimer(float startTime, int frames, SimpleCompletionCallback onComplete)
			: this(startTime, frames, (in CKInstant _) => onComplete()) { }

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			Counter -= 1;


			IsComplete = Counter <= 0;
			if (IsComplete) {
				float localTime = instant.localTime - StartTime;

				CKInstant timerInstant = new CKInstant(
					instant.queue,
					localTime,
					instant.deltaTime,
					instant.updateCount
				);
				onComplete?.Invoke(timerInstant);
			}
			return IsComplete;
		}
	}
}