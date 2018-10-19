using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using System;

namespace RayTracer.Materials {
    public interface Material {
        bool Scatter(Ray rayIn, HitRecord record, ref Vec3 attenuation, ref Ray scatteredRay, Random random);
    }
}
