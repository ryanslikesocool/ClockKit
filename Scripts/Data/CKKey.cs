using System;

namespace ClockKit {
    /// <summary>
    /// <c>CKKey</c>s are to be used as an identifier for update delegates and timers.
    ///
    /// You should never create a key manually.  Instead, store the key from the result of the <see cref="CKClock.AddDelegate(Queue, int, in IUpdatable))"/> and <see cref="CKClock.StartTimer(Queue, in ITimer)"/> functions.
    ///
    /// Each queue can support up to 4,294,967,296 (<c>UInt32.MaxValue + 1</c>) keys at the same time.
    /// Once a key has been removed from a queue with the <see cref="CKClock.RemoveDelegate(Queue, in CKKey)"/> and <see cref="CKClock.StopTimer(Queue, in CKKey)"/> functions, it becomes available for reuse by the queue.
    /// </summary>
    /// <remarks>
    /// Two queues may contain the same key.  Always specify a queue in a functoin call when possible.
    /// </remarks>
    public readonly struct CKKey : IEquatable<CKKey>, IComparable<CKKey> {
        public readonly UInt32 rawValue;

        public CKKey(in UInt32 rawValue) {
            this.rawValue = rawValue;
        }

        internal static readonly CKKey zero = new CKKey(0);

        // MARK: - Interface

        public bool Equals(CKKey other) => rawValue == other.rawValue;

        public int CompareTo(CKKey other) => rawValue.CompareTo(other.rawValue);

        // MARK: - Operator

        public static bool operator ==(CKKey lhs, CKKey rhs) => lhs.rawValue == rhs.rawValue;

        public static bool operator !=(CKKey lhs, CKKey rhs) => lhs.rawValue != rhs.rawValue;

        public static CKKey operator +(CKKey lhs, UInt32 rhs) => new CKKey(lhs.rawValue + rhs);

        // MARK: - Required

        public override bool Equals(object other) => other switch {
            CKKey otherKey => Equals(otherKey),
            _ => false
        };

        public override int GetHashCode() => rawValue.GetHashCode();
    }
}