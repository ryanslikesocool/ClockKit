// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;

namespace Timer {
    internal class SequenceObjectDelayWhile : AnySequenceObject {
        private readonly Func<bool> condition;
        private readonly Action action;

        public SequenceObjectDelayWhile(Func<bool> condition, Action action) {
            this.condition = condition;
            this.action = action;
        }

        public override IEnumerator Execute() {
            while (condition()) {
                yield return null;
            }

            action?.Invoke();
        }
    }
}