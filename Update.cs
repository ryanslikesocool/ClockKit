// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Collections;
using UnityEngine;

namespace Timer {
    public static class Update {
        /// <summary>
        /// Updates using [action] every frame until [duration] has passed, updating with [Time.deltaTime], and optionally calls [done] when finished.
        /// </summary>
        /// <param name="duration">The [duration] to do the [action] for.</param>
        /// <param name="action">The [action] to call every frame.</param>
        /// <param name="done">The optional Action to call when [done].</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float duration, Action<float> action, Action done = null) => For(duration, false, action, done);

        /// <summary>
        /// Updates using [action] every frame until [duration] has passed, updating with optionally [unscaledTime], and optionally calls [done] when finished.
        /// </summary>
        /// <param name="duration">The [duration] to do the [action] for.</param>
        /// <param name="unscaledTime">Toggles between scaled and unscaled time.</param>
        /// <param name="action">The [action] to call every frame.</param>
        /// <param name="done">The optional Action to call when [done].</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float duration, bool unscaledTime, Action<float> action, Action done = null) => Timer.Start(DoFor(duration, () => unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime, action, done));

        /// <summary>
        /// Updates using [action] every frame until [duration] has passed, updating with [delta], and optionally calls [done] when finished.
        /// </summary>
        /// <param name="duration">The [duration] to do the [action] for.</param>
        /// <param name="delta">The [delta] to update with.</param>
        /// <param name="action">The [action] to call every frame.</param>
        /// <param name="done">The optional Action to call when [done].</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine For(float duration, Func<float> delta, Action<float> action, Action done = null) => Timer.Start(DoFor(duration, delta, action, done));

        /// <summary>
        /// Updates using [action] every frame while [condition] is true, and optionally calls [done] when [condition] is false.
        /// </summary>
        /// <param name="condition">The [condition] to use while updating.</param>
        /// <param name="action">The [action] to call every frame.</param>
        /// <param name="done">The optional Action to call when [done].</param>
        /// <returns>Coroutine instance in case stopping is needed.</returns>
        public static Coroutine While(Func<bool> condition, Action action, Action done = null) => Timer.Start(DoWhile(condition, action, done));

        private static IEnumerator DoFor(float duration, Func<float> deltaTime, Action<float> action, Action done) {
            float time = 0;
            while (time < duration) {
                time += deltaTime();
                time = Mathf.Clamp01(time);
                action(time);
                yield return null;
            }
            if (done != null) { done(); }
        }

        private static IEnumerator DoWhile(Func<bool> condition, Action action, Action done) {
            while (condition()) {
                action();
                yield return null;
            }
            if (done != null) { done(); }
        }
    }
}