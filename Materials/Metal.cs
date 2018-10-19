using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using System;

namespace RayTracer.Materials {
    class Metal : Material {
        public Metal(Vec3 albedo, float fuzz) {
            this.Albedo = albedo;
            if (fuzz < 1) {
                this.Fuzz = fuzz;
            } else {
                this.Fuzz = 1;
            }
        }

        public bool Scatter(Ray rayIn, HitRecord record, ref Vec3 attenuation, ref Ray scatteredRay, Random random) {
            var reflectedDirection = Vec3.Reflect(rayIn.Direction.Normalized, record.Normal);
            var fuzzyDirection = reflectedDirection + this.Fuzz * GeometrySampler.RandomPointInUnitSphere(random);
            scatteredRay = new Ray(record.Point, fuzzyDirection);
            attenuation = this.Albedo;
            return scatteredRay.Direction.Dot(record.Normal) > 0;
        }

        public Vec3 Albedo;
        public float Fuzz;
    }
}
