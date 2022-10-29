// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    public static class Delay {
        /// <summary>
        /// Waits until the next frame, then calls the [action].
        /// </summary>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine Frame(Action action, int repeat = 1) => Frame(1, action, repeat);

        /// <summary>
        /// Waits for [count] frames, then calls the [action].
        /// </summary>
        /// <param name="count">The number of frames to wait.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine Frame(int count, Action action, int repeat = 1) => For(yieldInstruction: null, count, action, repeat);

        /// <summary>
        /// Waits for [wait] seconds, in scaled game time, then calls the [action].
        /// </summary>
        /// <param name="wait">The seconds to [wait] for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float wait, Action action, int repeat = 1) => For(wait, false, action, repeat);

        /// <summary>
        /// Waits for [wait] seconds, in optionally unscaled time, then calls the [action].
        /// </summary>
        /// <param name="wait">The seconds in optionally [unscaledTime] to [wait] for.</param>
        /// <param name="unscaledTime">Determines if unscaled or scaled game time is used.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float wait, bool unscaledTime, Action action, int repeat = 1) {
            return Timer.Start(Wait());

            IEnumerator Wait() {
                for (int i = 0; i < repeat; i++) {
                    if (unscaledTime) {
                        yield return new WaitForSecondsRealtime(wait);
                    } else {
                        yield return new WaitForSeconds(wait);
                    }
                    action?.Invoke();
                }
            }
        }

        /// <summary>
        /// Waits for [wait] value, increasing the timer by [delta] every frame, then calls the [action].
        /// </summary>
        /// <param name="wait">The value to [wait] for.</param>
        /// <param name="delta">The update rate to increase the timer by every frame.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float wait, Func<float> delta, Action action, int repeat = 1) {
            return Timer.Start(Wait());

            IEnumerator Wait() {
                for (int i = 0; i < repeat; i++) {
                    float time = 0;
                    while (time < wait) {
                        time += delta();
                        yield return null;
                    }
                }
                action?.Invoke();
            }
        }

        /// <summary>
        /// Waits for [customYieldInstruction] to complete [count] times, then calls the [action], repeated [repeat] times.
        /// </summary>
        /// <param name="customYieldInstruction">The CustomYieldInstruction to wait for.</param>
        /// <param name="count">The number of [customYieldIntruction] to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(CustomYieldInstruction customYieldInstruction, int count, Action action, int repeat = 1) {
            return Timer.Start(Wait());

            IEnumerator Wait() {
                for (int i = 0; i < repeat; i++) {
                    for (int j = 0; j < count; j++) {
                        yield return customYieldInstruction;
                    }
                    action();
                }
            }
        }

        /// <summary>
        /// Waits for [customYieldInstruction] to complete, then calls the [action], repeated [repeat] times.
        /// </summary>
        /// <param name="customYieldInstruction">The CustomYieldInstruction to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(CustomYieldInstruction customYieldInstruction, Action action, int repeat = 1) => For(customYieldInstruction: customYieldInstruction, 1, action, repeat);

        /// <summary>
        /// Waits for [yieldInstruction] to complete, then calls the [action].
        /// </summary>
        /// <param name="yieldInstruction">The YieldInstruction to wait for.</param>
        /// <param name="count">The number of [yieldInstruction] to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(YieldInstruction yieldInstruction, int count, Action action, int repeat = 1) {
            return Timer.Start(Wait());

            IEnumerator Wait() {
                for (int i = 0; i < repeat; i++) {
                    for (int j = 0; j < count; j++) {
                        yield return yieldInstruction;
                    }
                    action?.Invoke();
                }
            }
        }

        /// <summary>
        /// Waits for [yieldInstruction] to complete, then calls the [action].
        /// </summary>
        /// <param name="yieldInstruction">The YieldInstruction to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(YieldInstruction yieldInstruction, Action action, int repeat = 1) => For(yieldInstruction: yieldInstruction, 1, action, repeat);

        /// <summary>
        /// Waits while the [condition] is true.  Once the [condition] is false, the [action] is called.
        /// </summary>
        /// <param name="condition">The [condition] to wait for.</param>
        /// <param name="action">The [action] to do when the condition is false.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine While(Func<bool> condition, Action action) {
            return Timer.Start(Wait());

            IEnumerator Wait() {
                while (condition()) {
                    yield return null;
                }
                action?.Invoke();
            }
        }
    }
}