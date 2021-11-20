// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace Timer {
    internal class Timer : MonoBehaviour {
        private static Timer instance = null;
        public static Timer Shared {
            get {
                if (!(instance ?? false)) {
                    Create();
                }
                return instance;
            }
        }

#if ODIN_INSPECTOR_3
        [ReadOnly] public uint activeTimers = 0;
#else
        [HideInInspector] public uint activeTimers = 0;
#endif

        private void OnDisable() => Destroy(gameObject);
        private void OnApplicationQuit() => Destroy(gameObject);

        public static T Create<T>() where T : MonoBehaviour {
            if (!Shared.gameObject.TryGetComponent<T>(out T component)) {
                return Shared.gameObject.AddComponent<T>();
            }
            return component;
        }

        private static void Create() {
            if (Application.isPlaying) {
                instance = new GameObject("Timer").AddComponent<Timer>();
            }
        }
    }
}