using System;
using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using RayTracer.Mathy;

namespace RayTracer.Materials {
    public class Dielectric : Material {
        Random rand;
        public Dielectric(float refractionIndex) {
            this.RefractionIndex = refractionIndex;
            this.rand = new Random();
        } 

        public float RefractionIndex { get; }


        // https://graphics.stanford.edu/courses/cs148-10-summer/docs/2006--degreve--reflection_refraction.pdf
        public bool Scatter(Ray rayIn, HitRecord record, ref Vec3 attenuation, ref Ray scatteredRay, Random random) {
            
            var reflected = Vec3.Reflect(rayIn.Direction, record.Normal);
            float nInOverNOut;
            attenuation = new Vec3(1, 1, 1);
            Vec3 refracted = null;
            Vec3 outwardNormal;
            float cosine;
            float reflectProb;
            if (rayIn.Direction.Dot(record.Normal) > 0) {
                // Ray and normal point in same direction
                outwardNormal = -record.Normal;
                nInOverNOut = this.RefractionIndex; // pass from material to air nOut=1
                cosine = this.RefractionIndex * rayIn.Direction.Dot(record.Normal) / rayIn.Direction.Length;
            } else {
                // Ray and normal point in opposite directions
                outwardNormal = record.Normal;
                nInOverNOut = 1 / this.RefractionIndex; // Pass from air to material
                cosine = -rayIn.Direction.Dot(record.Normal) / rayIn.Direction.Length;
            }

            if (Vec3.Refract(rayIn.Direction, outwardNormal, nInOverNOut, ref refracted)) {
                reflectProb = Schlick(cosine);
            } else {
                reflectProb = 1;
            }

            if (random.NextDouble() < reflectProb) {
                scatteredRay = new Ray(record.Point, reflected);
            }else {
                scatteredRay = new Ray(record.Point, refracted);
            }
            return true;
        }

        // https://en.wikipedia.org/wiki/Schlick%27s_approximation
        private float Schlick(float cosine) {
            float r0 = (1 - this.RefractionIndex) / (1 + this.RefractionIndex);
            r0 = r0 * r0;
            return r0 + (1 - r0) * (float)Math.Pow((1-cosine), 5);
        }
    }
}
