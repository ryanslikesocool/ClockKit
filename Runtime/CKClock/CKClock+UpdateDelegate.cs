// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;

namespace ClockKit {
	public static partial class CKClock {
		// MARK: - Add Delegate

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="updateDelegate">The object to add to the queue.  Its <see cref="ICKUpdateDelegate.OnUpdate(in CKInstant)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddUpdateDelegate(
			CKQueue queue,
			int priority,
			in ICKUpdateDelegate updateDelegate
		)
			=> CKClockController.Shared.queues[queue].AddUpdateDelegate(priority, updateDelegate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="priority">The delegate priority.  Higher values are updated first.</param>
		/// <param name="updateDelegate">The object to add to the queue.  Its <see cref="ICKUpdateDelegate.OnUpdate(in CKInstant)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddUpdateDelegate(
			int priority,
			in ICKUpdateDelegate updateDelegate
		)
			=> AddUpdateDelegate(CKQueue.Default, priority, updateDelegate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="queue">The queue to update on.</param>
		/// <param name="updateDelegate">The object to add to the queue.  Its <see cref="ICKUpdateDelegate.OnUpdate(in CKInstant)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddUpdateDelegate(
			CKQueue queue,
			in ICKUpdateDelegate updateDelegate
		)
			=> AddUpdateDelegate(queue, 0, updateDelegate);

		/// <summary>
		/// Add an update delegate.
		/// </summary>
		/// <param name="updateDelegate">The object to add to the queue.  Its <see cref="ICKUpdateDelegate.OnUpdate(in CKInstant)"/> function will be called every update cycle.</param>
		/// <returns>The delegate key.</returns>
		public static CKKey AddUpdateDelegate(
			in ICKUpdateDelegate updateDelegate
		)
			=> AddUpdateDelegate(CKQueue.Default, 0, updateDelegate);

		// MARK: - Has Delegate

		/// <summary>
		/// Is the given key active on a queue and associated with an update delegate?
		/// </summary>
		/// <param name="key">The update delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if a queue contains an update delegate with the given key; <see langword="false"/> otherwise.</returns>
		public static bool HasUpdateDelegate(
			in CKKey key
		)
			=> CKClockController.Shared.queues[key.queue].HasUpdateDelegate(key);

		/// <summary>
		/// Is the given key active on a queue and associated with an update delegate?
		/// </summary>
		/// <param name="key">The update delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if a queue contains an update delegate with the given key; <see langword="false"/> otherwise.</returns>
		public static bool HasUpdateDelegate(
			in CKKey? key
		) {
			if (key is not CKKey _key) {
				return false;
			}
			return HasUpdateDelegate(_key);
		}

		/// <summary>
		/// Are the given keys active on any queues and associated with update delegates?
		/// </summary>
		/// <param name="keys">The update delegate keys, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns>In order for each key - <see langword="true"/> if a queue contains an update delegate with the given key; <see langword="false"/> otherwise.</returns>
		public static IEnumerable<bool> HasUpdateDelegates(
			in IEnumerable<CKKey> keys
		)
			=> keys.Map(key => HasUpdateDelegate(key));

		/// <summary>
		/// Are the given keys active on any queues and associated with update delegates?
		/// </summary>
		/// <param name="keys">The update delegate keys, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns>In order for each key - <see langword="true"/> if a queue contains an update delegate with the given key; <see langword="false"/> otherwise.</returns>
		public static IEnumerable<bool> HasUpdateDelegates(
			in IEnumerable<CKKey?> keys
		)
			=> keys.Map(key => HasUpdateDelegate(key));

		// MARK: - Remove Delegate

		/// <summary>
		/// Remove an update delegate with its key.
		/// </summary>
		/// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
		public static bool RemoveUpdateDelegate(
			in CKKey key
		)
			=> CKClockController.Shared.queues[key.queue].RemoveUpdateDelegate(key);

		/// <summary>
		/// Remove an update delegate with its key.
		/// </summary>
		/// <param name="key">The delegate's key, provided by <c>Clock.AddDelegate</c>.</param>
		/// <returns><see langword="true"/> if the delegate was successfully removed; <see langword="false"/> otherwise.</returns>
		public static bool RemoveUpdateDelegate(
			in CKKey? key
		) {
			if (key is not CKKey _key) {
				return false;
			}
			return RemoveUpdateDelegate(_key);
		}

		/// <summary>
		/// Remove all delegates from a given queue.
		/// </summary>
		/// <param name="queue">The queue to remove all delegates from.</param>
		public static void RemoveAllUpdateDelegates(
			CKQueue queue
		)
			=> CKClockController.Shared.queues[queue].RemoveAllUpdateDelegates();

		/// <summary>
		/// Remove all delegates from every queue.
		/// </summary>
		public static void RemoveAllUpdateDelegates() {
			foreach (CKQueue queue in CKClockController.Shared.queues.Keys) {
				RemoveAllUpdateDelegates(queue);
			}
		}
	}
}