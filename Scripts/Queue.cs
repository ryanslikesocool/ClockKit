using System;

namespace ClockKit {
    public enum Queue {
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