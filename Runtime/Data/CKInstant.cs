// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// Information about an instant of time.
	/// </summary>
	public readonly struct CKInstant {
		/// <summary>
		/// The queue that the information comes from.
		/// </summary>
		public readonly CKQueue queue;

		public readonly float localTime;

		/// <summary>
		/// The amount of time between the previous and current instant.
		/// </summary>
		public readonly float deltaTime;

		public readonly ulong updateCount;

		internal CKInstant(CKQueue queue, float localTime, float deltaTime, ulong updateCount) {
			this.queue = queue;
			this.localTime = localTime;
			this.deltaTime = deltaTime;
			this.updateCount = updateCount;
		}
	}
}