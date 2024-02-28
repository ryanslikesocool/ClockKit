// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKCompletableAnimationUpdatingTimer<Value, Animation> : ICKTimer where Animation : ICKCompletableAnimation<Value> {
		public delegate void UpdateCallback(Value value);
		public delegate void CompletionCallback(Value value);
		public delegate void SimpleCompletionCallback();

		public float StartTime { get; }

		private Animation animation; // { get; private set; } does not work here
		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public readonly bool IsComplete => animation.IsComplete;

		public CKCompletableAnimationUpdatingTimer(float startTime, in Animation animation, UpdateCallback onUpdate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.animation = animation;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
		}

		public CKCompletableAnimationUpdatingTimer(float startTime, in Animation animation, UpdateCallback onUpdate, SimpleCompletionCallback onComplete) : this(
			startTime: startTime,
			animation: animation,
			onUpdate: onUpdate,
			onComplete: (Value _) => onComplete?.Invoke()
		) { }

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			float localTime = instant.localTime - StartTime;
			Value value = animation.Evaluate(localTime, float.NaN);

			onUpdate(value);

			if (IsComplete) {
				onComplete?.Invoke(value);
			}
			return IsComplete;
		}
	}
}