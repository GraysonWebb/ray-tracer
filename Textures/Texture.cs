using RayTracer.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Textures {
    public interface Texture {
        Vec3 Value(float u, float v, Vec3 point);
    }
}
