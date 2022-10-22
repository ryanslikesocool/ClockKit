// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
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