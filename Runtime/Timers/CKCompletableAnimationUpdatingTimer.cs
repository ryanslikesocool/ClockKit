namespace ClockKit {
	public struct CKCompletableAnimationUpdatingTimer<Value, Animation> : ICKTimer where Animation : ICKCompletableAnimation<Value> {
		public delegate void UpdateCallback(Value value);
		public delegate void CompletionCallback(Value value);

		public float StartTime { get; }

		private Animation animation; // { get; private set; } does not work here
		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete => animation.IsComplete;

		public CKCompletableAnimationUpdatingTimer(float startTime, in Animation animation, UpdateCallback onUpdate) : this(startTime, animation, onUpdate, null) { }

		public CKCompletableAnimationUpdatingTimer(float startTime, in Animation animation, UpdateCallback onUpdate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.animation = animation;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
		}

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