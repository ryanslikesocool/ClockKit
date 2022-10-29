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

        public Sequence Wait(float seconds, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectWaitSeconds(seconds, unscaledTime);
            return Append(obj);
        }

        public Sequence Invoke(Action action) {
            AnySequenceObject obj = new SequenceObjectInvoke(action);
            return Append(obj);
        }

        public Sequence Coroutine(Func<Coroutine> action) {
            AnySequenceObject obj = new SequenceObjectCoroutine(action);
            return Append(obj);
        }

        // MARK: - Delay

        public Sequence DelaySeconds(float seconds, Action action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectDelaySeconds(seconds, action, unscaledTime);
            return Append(obj);
        }

        public Sequence DelayFrame(Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(1, action);
            return Append(obj);
        }

        public Sequence DelayFrames(int frames, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(frames, action);
            return Append(obj);
        }

        public Sequence DelayCustomYieldInstruction(CustomYieldInstruction customYieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayCustomYieldInstruction(customYieldInstruction, action);
            return Append(obj);
        }

        public Sequence DelayYieldInstruction(YieldInstruction yieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayYieldInstruction(yieldInstruction, action);
            return Append(obj);
        }

        public Sequence DelayWhile(Func<bool> condition, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayWhile(condition, action);
            return Append(obj);
        }

        // MARK: - Update

        public Sequence UpdateSeconds(float seconds, Action<float> action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, null, unscaledTime);
            return Append(obj);
        }

        public Sequence UpdateSeconds(float seconds, Action<float> action, Action done, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, done, unscaledTime);
            return Append(obj);
        }

        public Sequence UpdateFrames(int frames, Action<int> action, Action done = null) {
            AnySequenceObject obj = new SequenceObjectUpdateFrames(frames, action, done);
            return Append(obj);
        }
    }
}