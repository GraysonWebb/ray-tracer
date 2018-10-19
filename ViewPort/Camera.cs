using RayTracer.LinearAlgebra;
using System;

namespace RayTracer.ViewPort {
    public interface Camera {
        Ray GetRay(int x, int y, Random random);
    }
}
