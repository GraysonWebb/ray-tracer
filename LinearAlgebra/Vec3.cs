using System;

namespace RayTracer.LinearAlgebra {
    public class Vec3 {
        private float x;
        private float y;
        private float z;

        public unsafe Vec3() {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vec3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 vec){
            SetFrom(vec);
        }

        public void SetFrom(Vec3 vector) {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public float X {
            get => x;
            set {
                x = value;
            }
        }
        public float Y {
            get => y;
            set {
                y = value;
            }
        }
        public float Z {
            get => z;
            set {
                z = value;
            }
        }

        public float R {
            get => x;
            set {
                x = value;
            }
        }
        public float G {
            get => y;
            set {
                y = value;
            }
        }
        public float B {
            get => z;
            set {
                z = value;
            }
        }

        public float this[int key] {
            get {
                switch (key) {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.x;
                    default:
                        throw new ArgumentException(nameof(key));
                }
            } set {
                switch (key) {
                    case 0:
                        this.x = value;
                        return;
                    case 1:
                        this.y = value;
                        return;
                    case 2:
                        this.z = value;
                        return;
                    default:
                        throw new ArgumentException(nameof(key));
                }
            }
        }


        public float Length => (float) Math.Sqrt(LengthSquared);

        public float LengthSquared => (x * x + y * y + z * z);

        public Vec3 Normalized => this / this.Length;

        public void Normalize() {
            float scaleFactor = 1 / this.Length;
            x *= scaleFactor;
            y *= scaleFactor;
            z *= scaleFactor;
        }


        public static float Dot(Vec3 left, Vec3 right) {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        public float Dot(Vec3 right) {
            return x * right.x + y * right.y + z * right.z;
        }

        public static Vec3 Cross(Vec3 left, Vec3 right) {
            return new Vec3((left.y * right.z - left.z * right.y),
                (-(left.x * right.z - left.z * right.x)),
                (left.x * right.y - left.y * right.x));
        }

        public Vec3 Cross(Vec3 right) {
            return new Vec3((y * right.z - z * right.y),
                (-(x * right.z - z * right.x)),
                (x * right.y - y * right.x));
        }

        public static Vec3 Lerp(float t, Vec3 start, Vec3 end) {
            return (1 - t) * start + t * end;
        }

        public static Vec3 Reflect(Vec3 v, Vec3 normal) {
            return v - 2 * Dot(v, normal) * normal;
        }

        // https://graphics.stanford.edu/courses/cs148-10-summer/docs/2006--degreve--reflection_refraction.pdf
        public static bool Refract(Vec3 v, Vec3 normal, float nInOverNOut, ref Vec3 refracted) {
            var unitV = v.Normalized;
            float dt = Dot(unitV, normal);
            float discriminant = 1f - nInOverNOut * nInOverNOut * (1 - dt * dt);
            if (discriminant > 0) {
                refracted = nInOverNOut*(unitV - normal*dt) - normal * (float)Math.Sqrt(discriminant);
                return true;
            }
            return false;
        }

        public static Vec3 operator-(Vec3 right) {
            return new Vec3(-right.x, -right.y, -right.z);
        }

        public static Vec3 operator+(Vec3 left, Vec3 right) {
            return new Vec3(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        public static Vec3 operator-(Vec3 left, Vec3 right) {
            return new Vec3(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        public static void Subtract(Vec3 left, Vec3 right, Vec3 result) {
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
        }

        public static Vec3 operator*(Vec3 left, Vec3 right) {
            return new Vec3(left.x * right.x, left.y * right.y, left.z * right.z);
        }

        public static Vec3 operator*(Vec3 left, float right) {
            return new Vec3(left.x * right, left.y * right, left.z * right);
        }

        public static Vec3 operator*(float left, Vec3 right) {
            return new Vec3(left * right.x, left * right.y, left * right.z);
        }

        public static Vec3 operator/(Vec3 left, Vec3 right) {
            return new Vec3(left.x / right.x, left.y / right.y, left.z / right.z);
        }

        public static Vec3 operator/(Vec3 left, float right) {
            return new Vec3(left.x / right, left.y / right, left.z / right);
        }

        public static Vec3 operator/(float left, Vec3 right) {
            return new Vec3(left / right.x, left / right.y, left / right.z);
        }
    }
}
