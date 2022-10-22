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

        internal void Append(AnySequenceObject sequenceObject) => sequenceObjects.Add(sequenceObject);

        internal IEnumerator _Execute() {
            for (int i = 0; i < sequenceObjects.Count; i++) {
                AnySequenceObject sequenceObject = sequenceObjects[i];
                Coroutine coroutine = Timer.Start(sequenceObject.Execute());
                yield return coroutine;
            }
        }

        public static Sequence Create() => new Sequence();

        public Coroutine Execute() => Timer.Start(_Execute());

        // MARK: - Objects

        public Sequence Wait(float seconds, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectWaitSeconds(seconds, unscaledTime);
            Append(obj);
            return this;
        }

        // MARK: Delay

        public Sequence DelaySeconds(float seconds, Action action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectDelaySeconds(seconds, action, unscaledTime);
            Append(obj);
            return this;
        }

        public Sequence DelayFrame(Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(1, action);
            Append(obj);
            return this;
        }

        public Sequence DelayFrames(int frames, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(frames, action);
            Append(obj);
            return this;
        }

        public Sequence DelayCustomYieldInstruction(CustomYieldInstruction customYieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayCustomYieldInstruction(customYieldInstruction, action);
            Append(obj);
            return this;
        }

        public Sequence DelayYieldInstruction(YieldInstruction yieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayYieldInstruction(yieldInstruction, action);
            Append(obj);
            return this;
        }

        public Sequence DelayWhile(Func<bool> condition, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayWhile(condition, action);
            Append(obj);
            return this;
        }

        // MARK: Update

        public Sequence UpdateSeconds(float seconds, Action<float> action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, null, unscaledTime);
            Append(obj);
            return this;
        }

        public Sequence UpdateSeconds(float seconds, Action<float> action, Action done, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, done, unscaledTime);
            Append(obj);
            return this;
        }

        public Sequence UpdateFrames(int frames, Action<int> action, Action done = null) {
            AnySequenceObject obj = new SequenceObjectUpdateFrames(frames, action, done);
            Append(obj);
            return this;
        }

    }
}