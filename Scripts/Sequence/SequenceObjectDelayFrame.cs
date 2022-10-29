// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;

namespace Timer {
    internal class SequenceObjectDelayFrame : AnySequenceObject {
        private readonly int frames;
        private readonly Action action;

        public SequenceObjectDelayFrame(int frames, Action action) {
            this.frames = frames;
            this.action = action;
        }

        public override IEnumerator Execute() {
            for (int i = 0; i < frames; i++) {
                yield return null;
            }
            action?.Invoke();
        }
    }
}