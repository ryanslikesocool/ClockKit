// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;

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
}