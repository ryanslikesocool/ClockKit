// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	/// <summary>
	/// The base interface for an animation.
	/// </summary>
	/// <typeparam name="Value">The type of value to animate.</typeparam>
	public interface ICKAnimation<Value> {
		/// <summary>
		/// Evaluate the current value from the given time properties.
		/// </summary>
		/// <param name="localTime">The amount of time that has passed since the animation started.</param>
		/// <param name="percent">The completion percentage of the animation.  This may be NaN if the containing <see cref="ICKTimer"/> does not provide a percent.</param>
		/// <returns>The current animation value.</returns>
		Value Evaluate(float localTime, float percent);
	}
}