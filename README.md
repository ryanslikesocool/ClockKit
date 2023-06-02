# ClockKit
Timer functions for Unity

## Installation
**Recommended Installation** (Unity Package Manager)
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/ClockKit.git`

**Alternate Installation** (Not recommended)
- Get the latest [release](https://github.com/ryanslikesocool/ClockKit/releases)
- Open with the desired Unity project
- Import into the Plugins folder

## Usage
ClockKit will automatically create a game object and attach scripts the first time it's accessed.

### Essentials

\- __CKClock__\
The static `CKClock` class provides access to most functionality, including:
- starting and stopping timers
- adding and removing update delegates

\- __Queues__\
Three `CKQueue`s are provided by default, each associated with a Unity-provided update loop.
The Update queue is used by default unless otherwise specified.

\- __Clock Information__\
A `ClockInformation` struct is provided with essential current time information that can be used by both timers and delegates, including:
- queue
- time
- delta time
- update count

### Starting Timers
The `CKClock` class contains multiple functions and overloads to start timers.
```cs
CKClock.Delay(queue: Queue.Update, duration: 2.5f, onComplete: () => {
    Debug.Log("2.5 seconds have passed.");
})
```

Each function provides overloads allowing omission of some values, such as the queue.
```cs
// uses Queue.Default
Clock.Delay(duration: 2.5f, onComplete: () => { /* ... */ });
```

### Stopping Timers
All `Clock` functions that start timers return a `CKKey` object that can be used to stop the associated timer.
```cs
CKKey delayKey = CKClock.Delay(duration: 2.5f, onComplete: () => {
    Debug.Log("2.5 seconds have passed.");
});

bool success = CKClock.StopTimer(delayKey);
```
The `StopTimer` function returns a `bool`, returning `true` if a timer associated with the key exists and was stopped, or `false` if there was no timer.

By default, the `StopTimer` function will attempt to stop the timer on all queues.
An overload is provided that allows a timer to be stopped on a certain `CKQueue`.

A `StopTimers` function is also provided to stop multiple timers at once.

### Custom Timers
Implementing the `ITimer` or `IFixedDurationTimer` allows for timers with custom logic to be created.

The timer interfaces have an `OnUpdate` function.  Return `true` to mark the timer as complete and stop it.
```cs
// This example creates a timer that stops early if a value is true, or after certain amount of time has passed.

public struct MyCustomTimer: IFixedDurationTimer {
    public delegate bool CompletionPredicate(float elapsedTime);
    public delegate void CompletionCallback(float elapsedTime);

    public float StartTime { get; }
    public float Duration { get; }

    public readonly CompletionCallback completionPredicate;
    public readonly CompletionCallback onComplete;

    public bool IsComplete { get; private set; }

    public FixedDurationDelayingTimer(float startTime, float maxDuration, CompletionPredicate completionPredicate, CompletionCallback onComplete = null) {
        this.StartTime = startTime;
        this.Duration = maxDuration;
        this.completionPredicate = completionPredicate;
        this.onComplete = onComplete;
        this.IsComplete = false;
    }

    public bool OnUpdate(in ClockInformation information) {
        // Safeguard
        if (IsComplete) {
            return true;
        }

        // Calcualate the "local time" of the timer
        float localTime = information.time - StartTime;

        // Invoke the predicate
        IsComplete = completionPredicate(localTime);

        if (IsComplete) {
            Debug.Log("Timer predicate returned true.");
        }

        // Check if the max time has elapsed
        IsComplete |= localTime >= Duration;

        if (IsComplete) {
            onComplete?.Invoke(localTime);
        }
        return IsComplete;
    }
}
```
Custom timers can be started similarly to built-in timers...
```cs
MyCustomTimer.CompletionPredicate completionPredicate = _ => {
    bool isComplete = SomeFunction();
    return isComplete;
};

// Create the timer object
ITimer customTimer = new MyCustomTimer(
    startTime: Time.time,
    maxDuration: 5.0f,
    completionPredicate:
    onComplete: elapsedTime => { Debug.Log($"Timer is complete after {elapsedTime} seconds"); }
);

// Start the timer
CKKey customTimerKey = Clock.StartTimer(queue: Queue.LateUpdate, timer: customTimer);
```
... and stopped in the same way
```cs
Clock.StopTimer(queue: Queue.LateUpdate, key: customTimerKey);
```

### With Other Packages
ClockKit has optional support for another package.

If [EaseKit](https://github.com/ryanslikesocool/EaseKit) (3.0.0 or later) is included in the project, convenience functions to start timers with easings and interpolators are enabled.
