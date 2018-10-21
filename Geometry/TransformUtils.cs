using RayTracer.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Geometry {
    public static class TransformUtils {
        public static Vec3 OrthogonalBaseChange(Vec3 vec, Vec3 u, Vec3 v, Vec3 w) {
            Vec3 result = new Vec3(0,0,0);
            result.X = vec.Dot(u);
            result.Y = vec.Dot(v);
            result.Z = vec.Dot(w);
            return result;
        }
    }
}
