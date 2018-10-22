using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.LinearAlgebra;

namespace RayTracer.Textures {
    public class CheckerTexture : Texture {
        public CheckerTexture() { }

        public CheckerTexture(Texture evenTexture, Texture oddTexture) {
            this.EvenTexture = evenTexture;
            this.OddTexture = oddTexture;
        }

        public Texture EvenTexture { get; }

        public Texture OddTexture { get; }

        public Vec3 Value(float u, float v, Vec3 point) {
            float sines = (float)(Math.Sin(10 * point.X) * Math.Sin(10 * point.Y) * Math.Sin(10 * point.Z));

            if (sines < 0) {
                return this.OddTexture.Value(u, v, point);
            } else {
                return this.EvenTexture.Value(u, v, point);
            }
        }
    }
}
