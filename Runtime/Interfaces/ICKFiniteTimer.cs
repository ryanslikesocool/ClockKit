// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// Refines <see cref="ICKTimer"/> for timers to be complete after a set duration.
	/// </summary>
	/// <seealso cref="ICKTimer"/>
	public interface ICKFiniteTimer : ICKTimer {
		float Duration { get; }
	}
}