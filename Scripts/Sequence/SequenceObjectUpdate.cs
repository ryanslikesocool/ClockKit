// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    internal class SequenceObjectUpdateFrames : AnySequenceObject {
        private readonly int duration;
        private readonly Action<int> action;
        private readonly Action done;

        public SequenceObjectUpdateFrames(int duration, Action<int> action, Action done) {
            this.duration = duration;
            this.action = action;
            this.done = done;
        }

        public override IEnumerator Execute() {
            int frame = 0;
            while (frame < duration) {
                action?.Invoke(frame);
                frame++;
                yield return null;
            }
            done?.Invoke();
        }
    }

    internal class SequenceObjectUpdateWhile : AnySequenceObject {
        private readonly Func<bool> condition;
        private readonly Action action;
        private readonly Action done;

        public SequenceObjectUpdateWhile(Func<bool> condition, Action action, Action done) {
            this.condition = condition;
            this.action = action;
            this.done = done;
        }

        public override IEnumerator Execute() {
            while (condition()) {
                action?.Invoke();
                yield return null;
            }
            done?.Invoke();
        }
    }

    internal class SequenceObjectUpdateSeconds : AnySequenceObject {
        private readonly float duration;
        private readonly Action<float> action;
        private readonly Action done;
        private readonly bool unscaledTime;

        private float DeltaTime => unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        public SequenceObjectUpdateSeconds(float duration, Action<float> action, Action done, bool unscaledTime) {
            this.duration = duration;
            this.action = action;
            this.done = done;
            this.unscaledTime = unscaledTime;
        }

        public override IEnumerator Execute() {
            float time = 0;
            while (time < duration) {
                action?.Invoke(time);
                time += DeltaTime;
                yield return null;
            }
            done?.Invoke();
        }
    }

    internal class SequenceObjectDelaySeconds : AnySequenceObject {
        private readonly float seconds;
        private readonly Action action;
        private readonly bool unscaledTime;

        public SequenceObjectDelaySeconds(float seconds, Action action, bool unscaledTime) {
            this.seconds = seconds;
            this.action = action;
            this.unscaledTime = unscaledTime;
        }

        public override IEnumerator Execute() {
            if (unscaledTime) {
                yield return new WaitForSecondsRealtime(seconds);
            } else {
                yield return new WaitForSeconds(seconds);
            }

            action?.Invoke();
        }
    }
}