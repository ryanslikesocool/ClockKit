// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// Refines <see cref="ICKAnimation"/> for animations with a fixed duration.
	/// </summary>
	/// <typeparam name="Value">The type of value to animate.</typeparam>
	/// <seealso cref="ICKAnimation"/>
	public interface ICKFiniteAnimation<Value> : ICKAnimation<Value> {
		float Duration { get; }
	}
}