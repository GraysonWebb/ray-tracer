using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using RayTracer.Textures;
using System;

namespace RayTracer.Materials {
    public class Lambertian : Material {
        public Lambertian(Texture albedo) {
            this.Albedo = albedo;
        }

        public Texture Albedo;

        
        public bool Scatter(Ray rayIn, HitRecord record, ref Vec3 attenuation, ref Ray scatteredRay, Random random) {
            var bounceDirection = record.Normal + GeometrySampler.RandomPointInUnitSphere(random);
            scatteredRay = new Ray(record.Point, bounceDirection);
            attenuation = this.Albedo.Value(0, 0, record.Point);
            return true;
        }
    }
}
