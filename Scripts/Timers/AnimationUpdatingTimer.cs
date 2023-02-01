using System;
using Unity.Mathematics;

namespace ClockKit {
    public struct AnimationUpdatingTimer<Value, Animation> : IFixedDurationTimer where Animation : IFixedDurationAnimation<Value> {
        public delegate void UpdateCallback(Value value);
        public delegate void CompletionCallback(Value value);

        public float StartTime { get; }
        public float Duration => animation.Duration;

        public readonly Animation animation;
        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public AnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate) : this(startTime, animation, onUpdate, null) { }

        public AnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.animation = animation;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            IsComplete = false;
        }

        public bool Update(in Information information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;
            float percent = math.saturate(localTime / Duration);
            Value value = animation.Evaluate(percent);

            onUpdate(value);

            IsComplete = percent >= 1;
            if (IsComplete) {
                onComplete?.Invoke(value);
            }
            return IsComplete;
        }
    }
}