using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.LinearAlgebra;

namespace RayTracer.Textures {
    public class ConstantTexture : Texture {
        public ConstantTexture() {}

        public ConstantTexture(Vec3 color) {
            this.Color = color;
        }

        public Vec3 Color { get; }


        public Vec3 Value(float u, float v, Vec3 point) {
            return this.Color;
        }
    }
}
