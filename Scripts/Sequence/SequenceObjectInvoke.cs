// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;

namespace Timer {
    internal class SequenceObjectInvoke : AnySequenceObject {
        private readonly Action action;

        public SequenceObjectInvoke(Action action) {
            this.action = action;
        }

        public override IEnumerator Execute() {
            action?.Invoke();
            yield break;
        }
    }
}