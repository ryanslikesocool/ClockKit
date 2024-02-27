using System;
using UnityEngine;

namespace ClockKit {
	/// <summary>
	/// The type of object associated with a <see cref="CKKey"/>.
	/// </summary>
	/// <seealso cref="CKKey"/>
	public enum CKKeyAssociation : byte {
		/// <summary>
		/// Indicates that a <see cref="CKKey"/> is associated with a timer.
		/// </summary>
		Timer,
		/// <summary>
		/// Indicates that a <see cref="CKKey"/> is associated with an update delegate.
		/// </summary>
		UpdateDelegate,
	}
}