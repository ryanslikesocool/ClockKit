// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    internal class SequenceObjectDelayCustomYieldInstruction : AnySequenceObject {
        private readonly CustomYieldInstruction customYieldInstruction;
        private readonly Action action;

        public SequenceObjectDelayCustomYieldInstruction(CustomYieldInstruction customYieldInstruction, Action action) {
            this.customYieldInstruction = customYieldInstruction;
            this.action = action;
        }

        public override IEnumerator Execute() {
            yield return customYieldInstruction;
            action?.Invoke();
        }
    }

    internal class SequenceObjectDelayYieldInstruction : AnySequenceObject {
        private readonly YieldInstruction yieldInstruction;
        private readonly Action action;

        public SequenceObjectDelayYieldInstruction(YieldInstruction yieldInstruction, Action action) {
            this.yieldInstruction = yieldInstruction;
            this.action = action;
        }

        public override IEnumerator Execute() {
            yield return yieldInstruction;
            action?.Invoke();
        }
    }

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