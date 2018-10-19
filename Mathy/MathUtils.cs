using System;

namespace RayTracer.Mathy {
    public static class MathUtils {
        public static float PI = (float)Math.PI;

        public static float ToRad(float deg) {
            return 2 * PI * (deg / 360f);
        }

        public static float ToDeg(float rad) {
            return 360f * (rad / (2 * PI));
        }

        public static float MaxF(float a, float b) {
            return a > b ? a : b;
        }

        public static float MinF(float a, float b) {
            return a < b ? a : b;
        }
    }
}
