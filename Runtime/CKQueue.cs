// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public enum CKQueue {
		/// <summary>
		/// The <c>Update</c> queue corresponds to Unity's <c>Update</c> loop.
		/// </summary>
		Update,

		/// <summary>
		/// The <c>FixedUpdate</c> queue corresponds to Unity's <c>FixedUpdate</c> loop.
		/// </summary>
		FixedUpdate,

		/// <summary>
		/// The <c>LateUpdate</c> queue corresponds to Unity's <c>LateUpdate</c> loop.
		/// </summary>
		LateUpdate,

		/// <summary>
		/// The default update queue (<see cref="Update"/>).
		/// </summary>
		Default = Update,
	}
}