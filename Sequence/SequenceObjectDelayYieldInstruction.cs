// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
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
}