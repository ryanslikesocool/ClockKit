// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Collections;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace Timer {
    public class Timer : MonoBehaviour {
        private static Timer instance = null;
        public static Timer Shared {
            get {
                if (!Application.isPlaying) { return null; }

                if (!(instance ?? false)) {
                    Create();
                }
                return instance;
            }
        }

        private void OnDisable() => Destroy(gameObject);
        private void OnApplicationQuit() => Destroy(gameObject);

        private static void Create() {
            if (Application.isPlaying) {
                instance = new GameObject("Timer").AddComponent<Timer>();
            }
        }

        public static Coroutine Start(IEnumerator routine) => Shared.StartCoroutine(routine);

        public static void Stop(Coroutine coroutine) {
            if (coroutine != null) {
                Shared.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}