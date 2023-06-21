using System;
#if UNITY_MATHEMATICS
using Unity.Mathematics;
#else
using UnityEngine;
#endif

namespace ClockKit {
    public struct CKFiniteAnimationUpdatingTimer<Value, Animation> : ICKFiniteTimer where Animation : ICKFiniteAnimation<Value> {
        public delegate void UpdateCallback(Value value);
        public delegate void CompletionCallback(Value value);

        public float StartTime { get; }
        public float Duration => animation.Duration;

        public readonly Animation animation;
        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public CKFiniteAnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate) : this(startTime, animation, onUpdate, null) { }

        public CKFiniteAnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.animation = animation;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
            IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;
#if UNITY_MATHEMATICS
            float percent = math.saturate(localTime / Duration);
#else
            float percent = Mathf.Clamp(percent, 0f, 1f);
#endif
            Value value = animation.Evaluate(localTime, percent);

            onUpdate(value);

            IsComplete = percent >= 1;
            if (IsComplete) {
                onComplete?.Invoke(value);
            }
            return IsComplete;
        }
    }
}