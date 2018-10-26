using RayTracer.LinearAlgebra;
using System;
using System.Linq;

namespace RayTracer.Textures {
    public class Perlin {
        private Vec3[] randomVector;
        private int[] permutationX;
        private int[] permutationY;
        private int[] permutationZ;

        private Random random = new Random(123);

        public Perlin() {
            this.randomVector = GenerateRandomVectors();
            this.permutationX = GeneratePermutations();
            this.permutationY = GeneratePermutations();
            this.permutationZ = GeneratePermutations();
        }

        public float Turbulence(Vec3 point, int depth = 7) {
            float accum = 0;
            Vec3 temp = new Vec3(point);
            float weight = 1;
            for (int i = 0; i < depth; i++) {
                accum += weight * Noise(temp);
                weight *= 0.5f;
                temp *= 2;
            }

            return accum > 0 ? accum : -accum;
        }

        public float Noise(Vec3 point) {
            float u = point.X - (float)Math.Floor(point.X);
            float v = point.Y - (float)Math.Floor(point.Y);
            float w = point.Z - (float)Math.Floor(point.Z);
            
            int i = (int) Math.Floor(point.X);
            int j = (int) Math.Floor(point.Y);
            int k = (int) Math.Floor(point.Z);

            Vec3[,,] c = new Vec3[2,2,2];
            for (int di = 0; di < 2; di++) {
                for (int dj = 0; dj < 2; dj++) {
                    for (int dk = 0; dk < 2; dk++) {
                        c[di,dj,dk] = this.randomVector[this.permutationX[(i+di) & 255] ^ permutationY[(j+dj) & 255] ^ permutationZ[(k+dk) & 255]];
                    }
                }
            }
            return TrilinearInterpolation(c, u, v, w);
        }

        // https://en.wikipedia.org/wiki/Trilinear_interpolation
        private float TrilinearInterpolation(Vec3[,,] c, float u, float v, float w) {
            // https://en.wikipedia.org/wiki/Cubic_Hermite_spline
            float uu = u * u * (3 - 2 * u);
            float vv = v * v * (3 - 2 * v);
            float ww = w * w * (3 - 2 * w);
            float accumulated = 0;
            Vec3 weight = new Vec3();
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++) {
                    for (int k = 0; k < 2; k++) {
                        weight.X = u - i;
                        weight.Y = v - j;
                        weight.Z = w - k;
                        accumulated += (i * uu + (1 - i) * (1 - uu)) *
                                       (j * vv + (1 - j) * (1 - vv)) *
                                       (k * ww + (1 - k) * (1 - ww)) * c[i, j, k].Dot(weight);
                    }
                }
            }
            return accumulated;
        }

        private Vec3[] GenerateRandomVectors() {
            Vec3[] values = new Vec3[256];
            for (int i = 0; i < 256; i++) {
                values[i] = new Vec3 {
                    X = (float) (-1 + 2 * this.random.NextDouble()),
                    Y = (float) (-1 + 2 * this.random.NextDouble()),
                    Z = (float) (-1 + 2 * this.random.NextDouble())
                };
                values[i].Normalize();
            }
            return values;
        }

        private int[] GeneratePermutations() {
            int[] values = Enumerable.Range(0, 256).ToArray();
            Permute(values);
            return values;
        }

        private void Permute(int[] p) {
            for (int i = p.Length - 1; i > 0; i--) {
                int target = (int)(this.random.NextDouble() * (i + 1)); 
                int temp = p[i];
                p[i] = p[target];
                p[target] = temp;
            }
        }
    }
}
