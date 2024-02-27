// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// The base timer interface.
	/// </summary>
	public interface ICKTimer {
		bool IsComplete { get; }
		float StartTime { get; }

		bool OnUpdate(in CKInstant instant);
	}
}