// Developed with love by Ryan Boyer http://ryanjboyer.com <3

//FIXME: - Need to figure out how to get back to the main thread before calling the action.
/*using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public partial class Timer {
    /// <summary>
    /// Waits for [wait] seconds, in real time, then calls the [action].
    /// </summary>
    /// <param name="wait">The seconds to wait for.</param>
    /// <param name="action">The action to do when the timer is up.</param>
    /// <returns>Cancellation token source, used to cancel the timer if needed.</returns>
    public static CancellationTokenSource DelayAsync(float wait, Action action) {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken cancelToken = tokenSource.Token;

        Task task = Task.Run(async () => {
            await TimeSpan.FromSeconds(wait);
            if (!cancelToken.IsCancellationRequested) {
                //MARK: - ACTION
                action();
            }
            tokenSource = null;
        }, cancelToken);

        return tokenSource;
    }

    /// <summary>
    /// Stops a timer token if it's running.
    /// </summary>
    /// <param name="timer">The timer token to stop.</param>
    public static void Stop(CancellationTokenSource timer) {
        if (timer != null) {
            timer.Cancel();
        }
    }
}*/