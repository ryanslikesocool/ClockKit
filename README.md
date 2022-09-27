# Timer
Timer functions to delay calls for Unity

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

## Timer.Delay
`Timer.Delay` has static methods to start coroutines to act as delays.\
All `Timer.Delay` methods (besides `Stop`) return a `Coroutine`, which can be ignored, or stored for later to stop the timer if desired.\
All `Timer.Delay` methods (besides `Stop`) also include a final, optional `int` parameter for repeating timers a set number of times.

### Basic Usage
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

// And now I'll stop the timer.  This acts like cancelling the timer before it can call the lambda function.
Timer.Delay.Stop(timer);

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
Like `Timer.Delay`, all `Timer.Update` methods (besides `Stop`) return a `Coroutine`, which can be ignored, or stored for later to stop the timer if desired.\
All `Timer.Update` methods (besides `Stop`) have a final, optional `Action` parameter that calls the method once when the timer is up.

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
Timer.Update.Stop(timer);

bool condition = true;
Timer.Delay.For(1f, () => condition = false);
Timer.Update.While(() => condition, () => {
  // I'll be called every frame until the condition is false.
  // Since the condition is controlled with Timer.Delay, the condition will be false in 1 second.
});
```
