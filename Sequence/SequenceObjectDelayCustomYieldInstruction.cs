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
}