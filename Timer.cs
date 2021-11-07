// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static Timer instance = null;
    private static Timer Shared
    {
        get
        {
            if (!(instance ?? false))
            {
                CreateTimer();
            }
            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
        instance = null;
    }

    private static void CreateTimer() => instance = new GameObject("Timer").AddComponent<Timer>();

    public static void Stop(Coroutine timer)
    {
        if (timer != null)
        {
            Shared.StopCoroutine(timer);
        }
    }

    public static Coroutine Delay(Action action) => Delay(yieldInstruction: null, action);

    public static Coroutine Delay(float wait, Action action) => Delay(new WaitForSeconds(wait), action);

    public static Coroutine Delay(float wait, bool unscaledTime, Action action)
    {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            if (unscaledTime)
            {
                yield return new WaitForSecondsRealtime(wait);
            }
            else
            {
                yield return new WaitForSeconds(wait);
            }
            action();
        }
    }

    public static Coroutine Delay(float wait, Func<float> deltaTime, Action action)
    {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            float time = 0;
            while (time < wait)
            {
                time += deltaTime();
                yield return null;
            }
            action();
        }
    }

    public static Coroutine Delay(CustomYieldInstruction customYieldInstruction, Action action)
    {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return customYieldInstruction;
            action();
        }
    }

    public static Coroutine Delay(YieldInstruction yieldInstruction, Action action)
    {
        return Shared.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return yieldInstruction;
            action();
        }
    }
}