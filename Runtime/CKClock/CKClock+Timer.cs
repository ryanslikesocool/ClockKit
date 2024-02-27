// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;

namespace ClockKit {
	public static partial class CKClock {
		// MARK: - Start Timer

		/// <summary>
		/// Start a timer on the given queue.
		/// </summary>
		/// <param name="queue">The queue to run the timer on.</param>
		/// <param name="timer">The timer to update.</param>
		/// <returns>The timer key, which can be used to stop the timer early.</returns>
		public static CKKey StartTimer(
			CKQueue queue,
			in ICKTimer timer
		)
			=> CKClockController.Shared.queues[queue].StartTimer(timer);

		/// <summary>
		/// Start a timer on the default queue.
		/// </summary>
		/// <param name="timer">The timer to update.</param>
		/// <returns>The timer key, which can be used to stop the timer early.</returns>
		public static CKKey StartTimer(
			in ICKTimer timer
		)
			=> StartTimer(CKQueue.Default, timer);

		// MARK: - Has Timer

		/// <summary>
		/// Is the given key active on a queue and associated with a timer?
		/// </summary>
		/// <param name="key">The timer's key, provided by <see cref="CKClock.StatTimer"/>.</param>
		/// <returns><see langword="true"/> if a queue contains a timer with the given key; <see langword="false"/> otherwise.</returns>
		public static bool HasTimer(
			in CKKey key
		)
			=> CKClockController.Shared.queues[key.queue].HasTimer(key);

		/// <summary>
		/// Is the given key active on a queue and associated with a timer?
		/// </summary>
		/// <param name="key">The timer's key, provided by <see cref="CKClock.StatTimer"/>.</param>
		/// <returns><see langword="true"/> if a queue contains a timer with the given key; <see langword="false"/> otherwise.</returns>
		public static bool HasTimer(
			in CKKey? key
		) {
			if (key is not CKKey _key) {
				return false;
			}
			return HasTimer(_key);
		}

		/// <summary>
		/// Are the given keys active on any queues and associated with timers?
		/// </summary>
		/// <param name="keys">The timer keys, provided by <see cref="CKClock.StatTimer"/>.</param>
		/// <returns>In order for each key - <see langword="true"/> if a queue contains a timer with the given key; <see langword="false"/> otherwise.</returns>
		public static IEnumerable<bool> HasTimers(
			IEnumerable<CKKey> keys
		) => keys.Map(key => HasTimer(key));

		/// <summary>
		/// Are the given keys active on any queues and associated with timers?
		/// </summary>
		/// <param name="keys">The timer keys, provided by <see cref="CKClock.StatTimer"/>.</param>
		/// <returns>In order for each key - <see langword="true"/> if a queue contains a timer with the given key; <see langword="false"/> otherwise.</returns>
		public static IEnumerable<bool> HasTimers(
			IEnumerable<CKKey?> keys
		) => keys.Map(key => HasTimer(key));

		// MARK: - Stop Timer

		/// <summary>
		/// Stop a timer.
		/// </summary>
		/// <param name="key">The timer's key.</param>
		/// <returns><see langword="true"/> if a timer was successfully stopped; <see langword="false"/> otherwise.</returns>
		public static bool StopTimer(in CKKey key)
			=> CKClockController.Shared.queues[key.queue].StopTimer(key);

		/// <summary>
		/// Stop a timer.
		/// </summary>
		/// <param name="key">The timer's key.</param>
		/// <returns><see langword="true"/> if a timer was successfully stopped; <see langword="false"/> otherwise.</returns>
		public static bool StopTimer(in CKKey? key) {
			if (key is not CKKey _key) {
				return false;
			}
			return StopTimer(_key);
		}

		/// <summary>
		/// Stop multiple timers on all queues.
		/// </summary>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped on any queue or not.</returns>
		public static IEnumerable<bool> StopTimers(
			in IEnumerable<CKKey> keys
		)
			=> keys.Map(key => StopTimer(key));

		/// <summary>
		/// Stop multiple timers on all queues.
		/// </summary>
		/// <param name="keys">The timer keys to stop.</param>
		/// <returns>A collection of <see langword="bool"/>s, indicating whether the timer associated with a key at that index was stopped on any queue or not.</returns>
		public static IEnumerable<bool> StopTimers(
			in IEnumerable<CKKey?> keys
		)
			=> keys.Map(key => StopTimer(key));

		/// <summary>
		/// Stop all timers on a given queue.
		/// </summary>
		/// <param name="queue">The queue to stop all timers on.</param>
		public static void StopAllTimers(
			CKQueue queue
		)
			=> CKClockController.Shared.queues[queue].StopAllTimers();

		/// <summary>
		/// Stop all timers on every queue.
		/// </summary>
		public static void StopAllTimers() {
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				StopAllTimers(queue);
			}
		}
	}
}