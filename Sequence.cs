using System;
using UnityEngine;

namespace Timer {
    public static class Sequence {
        public static SequenceContainer Create() => new SequenceContainer();

        public static SequenceContainer Wait(this SequenceContainer container, float seconds, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectWaitSeconds(seconds, unscaledTime);
            container.Append(obj);
            return container;
        }

        // MARK: - Delay

        public static SequenceContainer DelaySeconds(this SequenceContainer container, float seconds, Action action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectDelaySeconds(seconds, action, unscaledTime);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer DelayFrame(this SequenceContainer container, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(1, action);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer DelayFrames(this SequenceContainer container, int frames, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayFrame(frames, action);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer DelayCustomYieldInstruction(this SequenceContainer container, CustomYieldInstruction customYieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayCustomYieldInstruction(customYieldInstruction, action);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer DelayYieldInstruction(this SequenceContainer container, YieldInstruction yieldInstruction, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayYieldInstruction(yieldInstruction, action);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer DelayWhile(this SequenceContainer container, Func<bool> condition, Action action) {
            AnySequenceObject obj = new SequenceObjectDelayWhile(condition, action);
            container.Append(obj);
            return container;
        }

        // MARK: - Update

        public static SequenceContainer UpdateSeconds(this SequenceContainer container, float seconds, Action<float> action, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, null, unscaledTime);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer UpdateSeconds(this SequenceContainer container, float seconds, Action<float> action, Action done, bool unscaledTime = false) {
            AnySequenceObject obj = new SequenceObjectUpdateSeconds(seconds, action, done, unscaledTime);
            container.Append(obj);
            return container;
        }

        public static SequenceContainer UpdateFrames(this SequenceContainer container, int frames, Action<int> action, Action done = null) {
            AnySequenceObject obj = new SequenceObjectUpdateFrames(frames, action, done);
            container.Append(obj);
            return container;
        }
    }
}