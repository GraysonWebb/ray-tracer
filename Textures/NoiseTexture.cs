using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.LinearAlgebra;

namespace RayTracer.Textures {
    public class NoiseTexture : Texture {
        
        private Perlin perlin = new Perlin();

        private float scale;

        public NoiseTexture() {
            this.scale = 4f;
        }

        public NoiseTexture(float scale) {
            this.scale = scale;
        }


        public Vec3 Value(float u, float v, Vec3 point) {
            //return new Vec3(1, 1, 1) * 0.5f * (1 + this.perlin.Noise(this.scale * point));
            //return new Vec3(1, 1, 1) * (this.perlin.Turbulence(this.scale * point));
            //return new Vec3(1, 1, 1) * 0.5f *  this.perlin.Turbulence(this.scale * point);

            return new Vec3(1, 1, 1) * 0.5f * (1 + (float)Math.Sin(this.scale * point.Z + 10 * perlin.Turbulence(point)));
        }
    }
}
