// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;

namespace Timer {
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
}