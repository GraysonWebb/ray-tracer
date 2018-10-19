using RayTracer.LinearAlgebra;
using System;

namespace RayTracer.Geometry {
    public static class GeometrySampler {
        public static Vec3 RandomPointInUnitSphere(Random random) {
            Vec3 point = new Vec3();
            do {
                point.X = (float)(2 * random.NextDouble() - 1);
                point.Y = (float)(2 * random.NextDouble() - 1);
                point.Z = (float)(2 * random.NextDouble() - 1);
            } while (point.LengthSquared >= 1);
            return point;
        } 

        public static Vec3 RandomPointInUnitDisk(Random random) {
            Vec3 point = new Vec3();
            do {
                point.X = (float)(2 * random.NextDouble() - 1);
                point.Y = (float)(2 * random.NextDouble() - 1);
                point.Z = 0;
            } while (point.LengthSquared >= 1);
            return point;
        }
    }
}
