// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    public class Delay : MonoBehaviour {
        private static Delay instance = null;
        private static Delay Shared {
            get {
                if (!(instance ?? false)) {
                    instance = Timer.Create<Delay>();
                }
                return instance;
            }
        }

        private void OnDestroy() => instance = null;

        /// <summary>
        /// Stops a timer coroutine if it's running.  The coroutine must have been started with one of the [Delay] methods.
        /// </summary>
        /// <param name="timer">The timer coroutine to stop.</param>
        public static void Stop(Coroutine timer) {
            if (timer != null) {
                Shared.StopCoroutine(timer);
                timer = null;
                Timer.Shared.activeTimers--;
            }
        }

        /// <summary>
        /// Waits until the next frame, then calls the [action].
        /// </summary>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine Frame(Action action, int repeat = 1) => For(yieldInstruction: null, 1, action, repeat);

        /// <summary>
        /// Waits for [count] frames, then calls the [action].
        /// </summary>
        /// <param name="count">The number of frames to wait.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine Frame(int count, Action action, int repeat = 1) => For(yieldInstruction: null, 1, action, repeat);

        /// <summary>
        /// Waits for [wait] seconds, in scaled game time, then calls the [action].
        /// </summary>
        /// <param name="wait">The seconds to [wait] for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float wait, Action action, int repeat = 1) => For(new WaitForSeconds(wait), 1, action, repeat);

        /// <summary>
        /// Waits for [wait] seconds, in optionally unscaled time, then calls the [action].
        /// </summary>
        /// <param name="wait">The seconds in optionally [unscaledTime] to [wait] for.</param>
        /// <param name="unscaledTime">Determines if unscaled or scaled game time is used.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float wait, bool unscaledTime, Action action, int repeat = 1) {
            return Shared.StartCoroutine(Wait());

            IEnumerator Wait() {
                Timer.Shared.activeTimers++;
                for (int i = 0; i < repeat; i++) {
                    if (unscaledTime) {
                        yield return new WaitForSecondsRealtime(wait);
                    } else {
                        yield return new WaitForSeconds(wait);
                    }
                    action();
                }
                Timer.Shared.activeTimers--;
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
            return Shared.StartCoroutine(Wait());

            IEnumerator Wait() {
                Timer.Shared.activeTimers++;
                for (int i = 0; i < repeat; i++) {
                    float time = 0;
                    while (time < wait) {
                        time += delta();
                        yield return null;
                    }
                }
                action();
                Timer.Shared.activeTimers--;
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
            return Shared.StartCoroutine(Wait());

            IEnumerator Wait() {
                Timer.Shared.activeTimers++;
                for (int i = 0; i < repeat; i++) {
                    for (int j = 0; j < count; j++) {
                        yield return customYieldInstruction;
                    }
                    action();
                }
                Timer.Shared.activeTimers--;
            }
        }

        /// <summary>
        /// Waits for [customYieldInstruction] to complete [count] times, then calls the [action], repeated [repeat] times.
        /// </summary>
        /// <param name="customYieldInstruction">The CustomYieldInstruction to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(CustomYieldInstruction customYieldInstruction, Action action, int repeat = 1) => For(customYieldInstruction, 1, action, repeat);

        /// <summary>
        /// Waits for [yieldInstruction] to complete, then calls the [action].
        /// </summary>
        /// <param name="yieldInstruction">The YieldInstruction to wait for.</param>
        /// <param name="count">The number of [yieldInstruction] to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(YieldInstruction yieldInstruction, int count, Action action, int repeat = 1) {
            return Shared.StartCoroutine(Wait());

            IEnumerator Wait() {
                Timer.Shared.activeTimers++;
                for (int i = 0; i < repeat; i++) {
                    for (int j = 0; j < count; i++) {
                        yield return yieldInstruction;
                    }
                    action();
                }
                Timer.Shared.activeTimers--;
            }
        }

        /// <summary>
        /// Waits for [yieldInstruction] to complete, then calls the [action].
        /// </summary>
        /// <param name="yieldInstruction">The YieldInstruction to wait for.</param>
        /// <param name="action">The [action] to do when the timer is up.</param>
        /// <param name="repeat">The number of times to [repeat] the timer.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(YieldInstruction yieldInstruction, Action action, int repeat = 1) => For(yieldInstruction, 1, action, repeat);

        /// <summary>
        /// Waits while the [condition] is true.  Once the [condition] is false, the [action] is called.
        /// </summary>
        /// <param name="condition">The [condition] to wait for.</param>
        /// <param name="action">The [action] to do when the condition is false.</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine While(Func<bool> condition, Action action) {
            return Shared.StartCoroutine(Wait());

            IEnumerator Wait() {
                Timer.Shared.activeTimers++;
                while (condition()) {
                    yield return null;
                }
                action();
                Timer.Shared.activeTimers--;
            }
        }
    }
}