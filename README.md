# Timer
Timer functions for Unity

## Installation
**Recommended Installation** (Unity Package Manager)\
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/Timer.git`

**Alternate Installation**\
- Get the latest [release](https://github.com/ryanslikesocool/Timer/releases)
- Open with the desired Unity project
- Import into the Plugins folder

## Usage
Timer will automatically create a game object and attach scripts to handle coroutine calls the first time it's needed in a scene.

## Timer.Timer
`Timer.Timer` is created automatically as a MonoBehaviour attached to a hidden GameObject.\
It handles all coroutines invoked from a `Timer.Delay` or `Timer.Update` function as well as `Sequence.Execute()`, and is only created once when it's first needed.\
`Timer.Timer` provides three static functions.
```cs
// Start any coroutine and attach it to the timer object.
IEnumerator someIEnumerator;
Timer.Timer.Start(someIEnumerator);

// Stop a couroutine that has been started from the Timer object.
// All coroutines in this package can be stopped with this function.
Coroutine someCoroutine;
Timer.Timer.Stop(someCoroutine);

// Stops all coroutines in a collection.
ICollection<Coroutine> someCoroutineCollection;
Timer.Timer.Stop(someCoroutineCollection);
```
An alternative to `Timer.Timer.Stop` is also provided.
```cs
Coroutine someCoroutine = Timer.Delay.Frame(() => { /* ... */ });
someCoroutine.Stop();
```
Coroutines stopped through the Timer class are done so in a safe manner.  If the couroutine is null, it will "fail" silently.


## Timer.Delay
`Timer.Delay` has static methods to start coroutines to act as delays.\
All `Timer.Delay` methods return a `Coroutine`, which can be ignored, or stored for later to stop the timer if desired.\
All `Timer.Delay` methods also include a final, optional `int` parameter for repeating timers a set number of times.
```cs
Timer.Delay.Frame(() => {
  // I'll be called during the next frame.
});

Timer.Delay.Frame(5, () => {
  // I'll be called in 5 frames.
});

Timer.Delay.For(1f, () => {
  // I'll be called after 1 second.
});

bool unscaled = true;
Timer.Delay.For(1f, unscaled, () => {
  // I'll be called after 1 second of *unscaled* time.
});

// Notice how the timer coroutine is stored here for later.
Coroutine timer = Timer.Delay.For(1f, () => 0.2f, () => {
  // I'll be called in 5 frames.
  // The first parameter (1f) is the duration.
  // The second parameter (0.2f) is the update rate.
  // 1f / 0.2f -> 5.
});

// And now I'll stop the timer.  This cancels the timer before it can call the lambda function.
Timer.Timer.Stop(timer);

bool condition = true;
Timer.Delay.For(1f, () => condition = false);
Timer.Delay.While(() => condition, () => {
  // I'll be called once the condition is false.
  // Since the condition is controlled with Timer.Delay, the condition will be false in 1 second.
});
```

### Custom Yields
Most Timer functions are straightforward in naming and usage.  The two that might be confusing are displayed in the block below.\
`CustomYieldInstruction` and `YieldInstruction` both act as ways to use things like `WaitForSeconds()`, but `YieldInstruction` only works for built-in Unity yields.\
Usage may look like this
```cs
// Notice how the first parameter is named here!
// This will work for Unity's built-in yield instructions.
Timer.Delay(yieldInstruction: new WaitForSeconds(3), () => {
  // Some code here.
});

// And this will work for your (or a pacakge's) custom yield instructions
Timer.Delay(customYieldInstruction: new WaitForGameSeconds(3), () => {
  // Some code here.
});
```

## Timer.Update
`Timer.Update` will call the same action over a period of time, which is useful for code-based animations.\
Like `Timer.Delay`, all `Timer.Update` methods return a `Coroutine`, which can be ignored, or stored for later to stop the timer if desired.\
All `Timer.Update` methods have a final, optional `Action` parameter that calls the method once when the timer is up.

```cs
Timer.Update.For(1f, time => {
  // Every frame, over the span of 1 second, the percent will be printed to the log.
  float percent = time / 1f;
  Debug.Log($"Timer percent is at {percent}");
});

bool unscaled = true;
Timer.Update.For(1f, unscaled, time => {
  // Every frame, over the span of 1 second of *unscaled* time, the percent will be printed to the log.
  float percent = time / 1f;
  Debug.Log($"Timer percent is at {percent}");
});

// Notice how the timer coroutine is stored here for later.
Coroutine timer = Timer.Update.For(1f, () => 0.2f, time => {
  // I'll be called every frame for 5 frames.
  // The first parameter (1f) is the duration.
  // The second parameter (0.2f) is the update rate.
  // 1f / 0.2f -> 5.
});

// And now I'll stop the timer.
Timer.Timer.Stop(timer);

bool condition = true;
Timer.Delay.For(1f, () => condition = false);
Timer.Update.While(() => condition, () => {
  // I'll be called every frame until the condition is false.
  // Since the condition is controlled with Timer.Delay, the condition will be false in 1 second.
});
```

## Timer.Sequence
`Timer.Sequence` provides an easy way to chain timers without creating a "pyramid of doom" with nested lambda functions.\
 Most other `Delay` and `Update` functions are provided as options, and behave in the same way as their un-sequenced counterparts.\
 A `Wait` function is available to force the sequence to wait without any lambdas.
```cs
// Call Timer.Sequence.Create() to create a new sequence.
Timer.Sequence sequence = Timer.Sequence.Create();

// Add a delay to the sequence, calling the lambda when complete.
sequence.DelaySeconds(1f, () => Debug.Log("I have waited one second."));

// Add a half second wait to the end of the sequence.  This will delay the next item in the sequence.
sequence.Wait(0.5f);

// Add an update to the end of the sequence.  Every frame, it will print out the current update time.
sequence.UpdateSeconds(2f, t => {
   // Count up to 2.0f
  Debug.Log($"Current update time: {t}");
});

// Execute the sequence.
Coroutine coroutine = sequence.Execute();

// Sequence items are executed in the order they were added.  The example above would look like this:
// Wait 1 second
// Print "I have waited one second."
// Wait 0.5 seconds
// Print "Current update time: 0"
// Print "Current update time: 0.016"
// Print "Current update time: 0.033"
// ... (keep printing the update time)
// Print "Current update time: 2"
```
Sequences can also be created without separating calls.  The example below would produce the same result as the example above
```cs
Coroutine coroutine = Timer.Sequence.Create()
  .DelaySeconds(1f, () => Debug.Log("I have waited one second."))
  .Wait(0.5f)
  .UpdateSeconds(2f, t => Debug.Log($"Current update time: {t}"))
  .Execute();
```
And, just like `Timer.Delay` and `Timer.Update` functions, Sequences can be stopped.
```cs
Timer.Timer.Stop(coroutine);
// or
coroutine.Stop();
```

### Custom Sequence Objects
Create a subclass of `AnySequenceObject` and override the required `Execute` function to create a custom sequence object.  This can be appended to a sequence and will execute in order, just like any other sequence item.\
```cs
public class CustomSequenceItem: AnySequenceObject {
  public override IEnumerator Execute() {
    /* body */
  }
}

Timer.Sequence sequence = Timer.Sequence.Create();
sequence.Append(new CustomSequenceItem());
sequence.Execute();
```
An extension method can also be created for easier coding.
```cs
public static Sequence AddCustomSequenceItem(this Sequence sequence) {
  AnySequenceObject obj = new CustomSequenceItem();
  sequence.Append(obj);
  return sequence;
}
```
