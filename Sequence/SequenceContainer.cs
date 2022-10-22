// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timer {
    public class SequenceContainer {
        internal List<AnySequenceObject> sequenceObjects;

        internal SequenceContainer() {
            sequenceObjects = new List<AnySequenceObject>();
        }

        internal void Append(AnySequenceObject sequenceObject) => sequenceObjects.Add(sequenceObject);

        internal IEnumerator _Execute() {
            for (int i = 0; i < sequenceObjects.Count; i++) {
                AnySequenceObject sequenceObject = sequenceObjects[i];
                Coroutine coroutine = Timer.Start(sequenceObject.Execute());
                yield return coroutine;
            }
        }

        public Coroutine Execute() => Timer.Start(_Execute());
    }
}