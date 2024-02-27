// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	/// <summary>
	/// A fixed duration animation that evaluates a Unity AnimationCurve every step.
	/// </summary>
	public readonly struct CKAnimationCurveAnimation : ICKFiniteAnimation<float> {
		public float Duration { get; }
		public readonly AnimationCurve animationCurve;
		private readonly float startTime;

		public CKAnimationCurveAnimation(in AnimationCurve animationCurve) {
			startTime = animationCurve.keys[0].time;
			this.Duration = animationCurve.keys[^1].time - startTime;
			this.animationCurve = animationCurve;
		}

		public float Evaluate(float localTime, float percent)
			=> animationCurve.Evaluate(startTime + localTime);

		public static implicit operator CKAnimationCurveAnimation(in AnimationCurve animationCurve) => new CKAnimationCurveAnimation(animationCurve);
	}
}