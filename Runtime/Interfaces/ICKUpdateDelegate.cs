// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// An update delegate to be used with <see cref="CKClock"/>.
	/// Update delegates can run on one or more <see cref="CKQueue"/>.
	/// </summary>
	public interface ICKUpdateDelegate {
		/// <summary>
		/// Called once every update step for each <see cref="CKQueue"/> the delegate is running on.
		/// </summary>
		/// <param name="instant">Information about the current update time.</param>
		void OnUpdate(in CKInstant instant);
	}
}