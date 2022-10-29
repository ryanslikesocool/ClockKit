// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    public class SequenceObjectCoroutine : AnySequenceObject {
        private readonly Func<Coroutine> action;

        public SequenceObjectCoroutine(Func<Coroutine> action) {
            this.action = action;
        }

        public override IEnumerator Execute() {
            yield return action();
        }
    }
}