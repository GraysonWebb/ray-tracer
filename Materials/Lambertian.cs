using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using System;

namespace RayTracer.Materials {
    public class Lambertian : Material {
        public Lambertian(Vec3 albedo) {
            this.Albedo = albedo;
        }

        public Vec3 Albedo;

        
        public bool Scatter(Ray rayIn, HitRecord record, ref Vec3 attenuation, ref Ray scatteredRay, Random random) {
            var bounceDirection = record.Normal + GeometrySampler.RandomPointInUnitSphere(random);
            scatteredRay = new Ray(record.Point, bounceDirection);
            attenuation = this.Albedo;
            return true;
        }
    }
}
