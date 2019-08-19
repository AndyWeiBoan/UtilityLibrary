using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace UtilityLibaray {
    public static class CacheExtensions {

        private static Dictionary<string, object> keyLocks = new Dictionary<string, object>();

        public static T GetOrAddWithLock<T>(this MemoryCache cache, string key, Func<T> callback) where T : class {

            var value = cache.Get(key);
            if (value == null) {

                if (!keyLocks.ContainsKey(key)) {
                    lock (keyLocks) {
                        if (!keyLocks.ContainsKey(key)) {
                            keyLocks.Add(key, new object());
                        }
                    }
                }

                lock (keyLocks[key]) {

                    if ((value = cache.Get(key)) == null
                        && (value = callback()) != null) {
                        cache.Set(key: key, value: value, policy: new CacheItemPolicy { });
                    } else {

                        keyLocks.Remove(key);
                    }
                }

            }
            return (T)value;
        }
    }
}
