// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timer {
    public class Sequence {
        internal List<AnySequenceObject> sequenceObjects;

        internal Sequence() {
            sequenceObjects = new List<AnySequenceObject>();
        }

        public Sequence Append(AnySequenceObject sequenceObject) {
            sequenceObjects.Add(sequenceObject);
            return this;
        }

        internal IEnumerator _Execute() {
            for (int i = 0; i < sequenceObjects.Count; i++) {
                AnySequenceObject sequenceObject = sequenceObjects[i];
                Coroutine coroutine = Timer.Start(sequenceObject.Execute());
                yield return coroutine;
            }
        }

        public static Sequence Create() => new Sequence();

        public Coroutine Execute() => Timer.Start(_Execute());

        // MARK: - Utility

        public Sequence Invoke(Action action) => Append(new SequenceObjectInvoke(action));

        public Sequence Coroutine(Func<Coroutine> action) => Append(new SequenceObjectCoroutine(action));

        // MARK: - Wait

        public Sequence Wait(float seconds, bool unscaledTime = false) => Append(new SequenceObjectWaitSeconds(seconds, unscaledTime));

        public Sequence WaitFrames(int frames) => Append(new SequenceObjectWaitFrames(frames));

        public Sequence WaitWhile(Func<bool> condition) => Append(new SequenceObjectWaitWhile(condition));

        // MARK: - Delay

        public Sequence DelaySeconds(float seconds, Action action, bool unscaledTime = false) => Append(new SequenceObjectDelaySeconds(seconds, action, unscaledTime));

        public Sequence DelayFrame(Action action) => Append(new SequenceObjectDelayFrame(1, action));

        public Sequence DelayFrames(int frames, Action action) => Append(new SequenceObjectDelayFrame(frames, action));

        public Sequence DelayCustomYieldInstruction(CustomYieldInstruction customYieldInstruction, Action action) => Append(new SequenceObjectDelayCustomYieldInstruction(customYieldInstruction, action));

        public Sequence DelayYieldInstruction(YieldInstruction yieldInstruction, Action action) => Append(new SequenceObjectDelayYieldInstruction(yieldInstruction, action));

        public Sequence DelayWhile(Func<bool> condition, Action action) => Append(new SequenceObjectDelayWhile(condition, action));

        // MARK: - Update

        public Sequence UpdateSeconds(float seconds, Action<float> action, bool unscaledTime = false) => Append(new SequenceObjectUpdateSeconds(seconds, action, null, unscaledTime));

        public Sequence UpdateSeconds(float seconds, Action<float> action, Action done, bool unscaledTime = false) => Append(new SequenceObjectUpdateSeconds(seconds, action, done, unscaledTime));

        public Sequence UpdateFrames(int frames, Action<int> action, Action done = null) => Append(new SequenceObjectUpdateFrames(frames, action, done));
    }
}