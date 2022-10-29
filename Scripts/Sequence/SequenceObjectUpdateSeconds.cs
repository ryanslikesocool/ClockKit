// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
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
}