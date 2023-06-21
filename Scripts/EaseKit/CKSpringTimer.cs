#if EASEKIT_3_1
using EaseKit;

namespace ClockKit {
    /// <summary>
    /// A <see cref="Spring"/>-based <see cref="ICKTimer"/> that provides more information for advanced use, such as retrieving the spring state.
    /// <br/>
    /// For basic <see cref="Spring"/> evaulation, see <see cref="CKSpringAnimation"/>.
    /// </summary>
    /// <seealso cref="Spring"/>
    /// <seealso cref="CKSpringAnimation"/>
    public struct CKSpringTimer : ICKTimer {
        public delegate void UpdateCallback(in Spring.Solver.State state);
        public delegate void CompletionCallback(in Spring.Solver.State state);

        public float StartTime { get; }

        public readonly UpdateCallback onUpdate;
        public readonly CompletionCallback onComplete;

        public bool IsComplete => state.IsComplete;

        private readonly Spring.Solver solver;
        private Spring.Solver.State state;

        public CKSpringTimer(
            float startTime,
            in Spring spring,
            UpdateCallback onUpdate,
            CompletionCallback onComplete = null
        ) : this(startTime, spring.CreateSolver(), onUpdate, onComplete) { }

        public CKSpringTimer(
            float startTime,
            in Spring spring,
            float initialVelocity,
            UpdateCallback onUpdate,
            CompletionCallback onComplete = null
        ) : this(startTime, spring.CreateSolver(), initialVelocity, onUpdate, onComplete) { }

        public CKSpringTimer(
            float startTime,
            in Spring.Solver solver,
            UpdateCallback onUpdate,
            CompletionCallback onComplete = null
        ) : this(startTime, solver, 0, onUpdate, onComplete) { }

        public CKSpringTimer(
            float startTime,
            in Spring.Solver solver,
            float initialVelocity,
            UpdateCallback onUpdate,
            CompletionCallback onComplete = null
        ) : this(startTime, solver, solver.CreateState(initialVelocity), onUpdate, onComplete) { }

        public CKSpringTimer(
            float startTime,
            in Spring.Solver solver,
            in Spring.Solver.State state,
            UpdateCallback onUpdate,
            CompletionCallback onComplete = null
        ) {
            this.StartTime = startTime;
            this.solver = solver;
            this.state = state;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            float localTime = information.time - StartTime;

            solver.Evaluate(localTime, ref state);
            onUpdate?.Invoke(state);

            if (IsComplete) {
                onComplete?.Invoke(state);
            }
            return IsComplete;
        }
    }
}
#endif