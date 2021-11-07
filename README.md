# Timer
Timer functions to delay calls for Unity

## Installation
**RECOMMENDED INSTALLATION**\
Add via the Unity Package Manager\
"Add package from git URL..."\
`https://github.com/ryanslikesocool/Timer.git`\
Add

**Not-so Recommended Installation**\
Get the latest [release](https://github.com/ryanslikesocool/Timer/releases)\
Open with the desired Unity project\
Import into the Plugins folder

## Usage
Timer will automatically create a gameobject to handle coroutine calls the first time it is needed in a scene.\
Timer has static methods to start coroutines to act as delays.\
All `Timer.Delay` functions return a `Coroutine`, which can be ignored, or stored for later to stop the timer if desired.

### Basic Usage
```cs
Timer.Delay(() => {
  // i'll be called during the next frame
});

Timer.Delay(1f, () => {
  // i'll be called after 1 second
});

bool unscaled = true;
Timer.Delay(1f, unscaled, () => {
  // i'll be called after 1 second of *unscaled* time
});

// notice how the timer coroutine is store here for later
Coroutine timer = Timer.Delay(1f, () => 0.2f, () => {
  // i'll be called in 5 frames
  // the first parameter (1f) is the duration
  // the second parameter (0.2f) is the update rate
  // 1f / 0.2f -> 5
});

// and now i'll stop the timer.  this acts like cancelling the timer before it can call the lambda function
Timer.Stop(timer);
```

### Custom Yields
Most Timer functions are straightforward in naming and usage.  The two that might be confusing are displayed in the block below.\
`CustomYieldInstruction` and `YieldInstruction` both act as ways to use things like `WaitForSeconds()`, but `YieldInstruction` only works for built-in Unity yields.\
Usage may look like this
```cs
// notice how the first parameter is named here!
// this will work for Unity's built-in yield instructions
Timer.Delay(yieldInstruction: new WaitForSeconds(3), () => {
  // some code here
});

// and this will work for your (or a pacakge's) custom yield instructions
Timer.Delay(customYieldInstruction: new WaitForGameSeconds(3), () => {
  // some code here
});
```
