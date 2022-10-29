// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace Timer {
    public static class Extensions {
        public static void Stop(this Coroutine coroutine) => Timer.Stop(coroutine);
    }
}