// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Collections.Generic;

namespace ClockKit {
	public struct CKSequenceTimer : ICKTimer {
		public delegate void CompletionCallback();

		public float StartTime { get; private set; }

		public readonly IList<Func<ICKTimer>> timerBuilders;

		public ICKTimer ActiveTimer { get; private set; }
		public int ActiveTimerIndex { get; private set; }

		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKSequenceTimer(float startTime, IList<Func<ICKTimer>> timerBuilders, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.timerBuilders = timerBuilders;
			this.onComplete = onComplete;
			this.ActiveTimerIndex = 0;
			this.ActiveTimer = timerBuilders[0]();
			this.IsComplete = false;
		}

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			bool isCurrentTimerComplete = ActiveTimer.OnUpdate(instant);
			if (isCurrentTimerComplete) {
				ActiveTimerIndex++;
				if (ActiveTimerIndex < timerBuilders.Count) {
					ActiveTimer = timerBuilders[ActiveTimerIndex]();
				}
			}

			IsComplete = isCurrentTimerComplete && ActiveTimerIndex == timerBuilders.Count;
			if (IsComplete) {
				onComplete?.Invoke();
			}
			return IsComplete;
		}
	}
}