// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

public partial class Timer : MonoBehaviour {
    private static Timer instance = null;
    private static Timer Shared {
        get {
            if (!(instance ?? false) && Application.isPlaying) {
                CreateTimer();
            }
            return instance;
        }
    }

    private void OnDestroy() {
        instance = null;
    }

    private static void CreateTimer() => instance = new GameObject("Timer").AddComponent<Timer>();

    /// <summary>
    /// Stops a timer coroutine if it's running.  The coroutine must have been started with one of the [Timer.Delay] methods.
    /// </summary>
    /// <param name="timer">The timer coroutine to stop.</param>
    public static void Stop(Coroutine timer) {
        if (timer != null) {
            Shared?.StopCoroutine(timer);
        }
    }

    /// <summary>
    /// Waits until the next frame, then calls the [action].
    /// </summary>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(Action action) => Delay(yieldInstruction: null, action);

    /// <summary>
    /// Waits for [wait] seconds, in scaled game time, then calls the [action].
    /// </summary>
    /// <param name="wait">The seconds to wait for.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(float wait, Action action) => Delay(new WaitForSeconds(wait), action);

    /// <summary>
    /// Waits for [wait] seconds, in optionally unscaled time, then calls the [action].
    /// </summary>
    /// <param name="wait">The seconds in optionally [unscaledTime] to wait for.</param>
    /// <param name="unscaledTime">Determines if unscaled or scaled game time is used.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(float wait, bool unscaledTime, Action action) {
        return Shared?.StartCoroutine(Wait());

        IEnumerator Wait() {
            if (unscaledTime) {
                yield return new WaitForSecondsRealtime(wait);
            } else {
                yield return new WaitForSeconds(wait);
            }
            action();
        }
    }

    /// <summary>
    /// Waits for [wait] value, increasing the timer by [delta] every frame, then calls the [action].
    /// </summary>
    /// <param name="wait">The value to wait for.</param>
    /// <param name="delta">The update rate to increase the timer by every frame.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(float wait, Func<float> delta, Action action) {
        return Shared?.StartCoroutine(Wait());

        IEnumerator Wait() {
            float time = 0;
            while (time < wait) {
                time += delta();
                yield return null;
            }
            action();
        }
    }

    /// <summary>
    /// Waits for [customYieldInstruction] to complete, then calls the [action].
    /// </summary>
    /// <param name="customYieldInstruction">The CustomYieldInstruction to wait for.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(CustomYieldInstruction customYieldInstruction, Action action) {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait() {
            yield return customYieldInstruction;
            action();
        }
    }

    /// <summary>
    /// Waits for [yieldInstruction] to complete, then calls the [action].
    /// </summary>
    /// <param name="yieldInstruction">The YieldInstruction to wait for.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Coroutine instance in case stopping is needed.</returns>
    public static Coroutine Delay(YieldInstruction yieldInstruction, Action action) {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait() {
            yield return yieldInstruction;
            action();
        }
    }
}
