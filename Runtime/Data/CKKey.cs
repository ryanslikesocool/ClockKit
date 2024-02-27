// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;

namespace ClockKit {
	/// <summary>
	/// <c>CKKey</c>s are to be used as an identifier for update delegates and timers.
	///
	/// You should never create a key manually.  Instead, store the key from the result of the <see cref="CKClock.AddDelegate(Queue, int, in ICKUpdatable))"/> and <see cref="CKClock.StartTimer(Queue, in ICKTimer)"/> functions.
	///
	/// Each queue can support up to 4,294,967,296 (<c>UInt32.MaxValue + 1</c>) keys at the same time.
	/// Once a key has been removed from a queue with the <see cref="CKClock.RemoveDelegate(Queue, in CKKey)"/> and <see cref="CKClock.StopTimer(Queue, in CKKey)"/> functions, it becomes available for reuse by the queue.
	/// </summary>
	/// <remarks>
	/// Two queues may contain the same key.  Always specify a queue in a function call when possible.
	/// </remarks>
	public readonly struct CKKey : IEquatable<CKKey>, IComparable<CKKey> {
		/// <summary>
		/// The queue this key originated from.
		/// </summary>
		public readonly CKQueue queue;

		/// <summary>
		/// The type of object this key is associated with.
		/// </summary>.
		public readonly CKKeyAssociation association;

		internal readonly uint rawValue;

		internal CKKey(CKQueue queue, CKKeyAssociation keyAssociation, uint rawValue) {
			this.queue = queue;
			this.association = keyAssociation;
			this.rawValue = rawValue;
		}

		// MARK: - Interface

		public bool Equals(CKKey other) => queue == other.queue && association == other.association && rawValue == other.rawValue;

		public int CompareTo(CKKey other) => rawValue.CompareTo(other.rawValue);

		// MARK: - Operator

		public static bool operator ==(CKKey lhs, CKKey rhs) => lhs.rawValue == rhs.rawValue;

		public static bool operator !=(CKKey lhs, CKKey rhs) => lhs.rawValue != rhs.rawValue;

		public static CKKey operator +(CKKey lhs, uint rhs) => new CKKey(lhs.queue, lhs.association, lhs.rawValue + rhs);

		// MARK: - Required

		public override bool Equals(object other) => other switch {
			CKKey otherKey => Equals(otherKey),
			_ => false
		};

		public override int GetHashCode() => rawValue.GetHashCode();
	}
}