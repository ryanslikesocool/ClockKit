// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    internal class SequenceObjectWaitSeconds : AnySequenceObject {
        private readonly float seconds;
        private readonly bool unscaledTime;

        public SequenceObjectWaitSeconds(float seconds, bool unscaledTime) {
            this.seconds = seconds;
            this.unscaledTime = unscaledTime;
        }

        public override IEnumerator Execute() {
            if (unscaledTime) {
                yield return new WaitForSecondsRealtime(seconds);
            } else {
                yield return new WaitForSeconds(seconds);
            }
        }
    }
}