using System;
using System.Threading;

namespace RayTracer.Mathy {
    public static class StaticRandom {
        private static readonly Random globalRandom = new Random();
        private static readonly object globalLock = new object();

        private static readonly ThreadLocal<Random> threadRandom = new ThreadLocal<Random>(NewRandom);

        public static Random NewRandom() {
            lock (globalLock) {
                return new Random(globalRandom.Next());
            }
        }

        private static Random Instance { get { return threadRandom.Value; } }

        //public static double Rand() {
        //    return Instance.NextDouble();
        //}
    }
}
