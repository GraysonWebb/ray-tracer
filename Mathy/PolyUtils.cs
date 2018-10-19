using System;

namespace RayTracer.Mathy {
    // Utils for polynomials.
    public class PolyUtils {
        // p(x) = a*x^2 + b*x + c
        public static float Discriminant(float a, float b, float c) {
            return b*b - 4 * a * c;
        }

        public static bool HasRealRoot(float a, float b, float c) {
            return Discriminant(a,b,c) >= 0;
        }

        public static float FirstRoot(float a, float b, float c) {
            return (-b - (float)Math.Sqrt(Discriminant(a,b,c))) /  (2 * a);
        }

        public static float SecondRoot(float a, float b, float c) {
            return (-b + (float)Math.Sqrt(Discriminant(a, b, c))) / (2 * a);
        }
    }
}
