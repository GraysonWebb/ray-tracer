using RayTracer.LinearAlgebra;
using System;
using RayTracer.Mathy;

namespace RayTracer.Geometry {
    public class BoundingBox {
        private Vec3[] bounds = new Vec3[2]; 

        public BoundingBox() {
        }

        // It must be that a < b for all coordinates!
        public BoundingBox(Vec3 a, Vec3 b) {
            this.Min = a;
            this.Max = b;

            bounds[0] = this.Min;
            bounds[1] = this.Max;

            if (a.X > b.X || a.Y > b.Y || a.Z > b.Z) {
                throw new ArgumentException("a is not before b in bounding box constructor!");
            }
        }

        public Vec3 Min { get; }

        public Vec3 Max { get; }

        // https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-box-intersection
        public bool Hit(Ray ray, float tMin, float tMax) {
            // X
            float txMin = (bounds[ray.InvertedDirectionSign[0]].X - ray.Origin.X) * ray.InvertedDirection.X;
            float txMax = (bounds[1 - ray.InvertedDirectionSign[0]].X - ray.Origin.X) * ray.InvertedDirection.X;

            if (tMin > txMax || txMin > tMax) {
                return false;
            }
            if (txMin > tMin) {
                tMin = txMin;
            }
            if (txMax < tMax) {
                tMax = txMax;
            }

            // Y
            float tyMin = (bounds[ray.InvertedDirectionSign[1]].Y - ray.Origin.Y) * ray.InvertedDirection.Y;
            float tyMax = (bounds[1 - ray.InvertedDirectionSign[1]].Y - ray.Origin.Y) * ray.InvertedDirection.Y;

            if (tMin > tyMax || tyMin > tMax) {
                return false;
            }
            if (tyMin > tMin) {
                tMin = tyMin;
            }
            if (tyMax < tMax) {
                tMax = tyMax;
            }

            // Z
            float tzMin = (bounds[ray.InvertedDirectionSign[2]].Z - ray.Origin.Z) * ray.InvertedDirection.Z;
            float tzMax = (bounds[1 - ray.InvertedDirectionSign[2]].Z - ray.Origin.Z) * ray.InvertedDirection.Z;

            if (tMin > tzMax || tzMin > tMax) {
                return false;
            }

            //for (int dim = 0; dim < 3; dim++) {
            //    float invD = 1 / ray.Direction[dim];
            //    float t0 = (Min[dim] - ray.Origin[dim]) * invD;
            //    float t1 = (Max[dim] - ray.Origin[dim]) * invD;
            //    if (invD < 0.0f) {
            //        float temp = t0;
            //        t0 = t1;
            //        t1 = temp;
            //    }
            //    tMin = t0 > tMin ? t0 : tMin;
            //    tMax = t1 < tMax ? t1 : tMax;
            //    if (tMax <= tMin) {
            //        return false;
            //    }
            //}


            return true;
        }

        public static BoundingBox SurroundingBox(BoundingBox box0, BoundingBox box1) {
            Vec3 min = new Vec3(MathUtils.MinF(box0.Min.X, box1.Min.X),
                MathUtils.MinF(box0.Min.Y, box1.Min.Y),
                MathUtils.MinF(box0.Min.Z, box1.Min.Z));
            Vec3 max = new Vec3(MathUtils.MaxF(box0.Max.X, box1.Max.X),
                MathUtils.MaxF(box0.Max.Y, box1.Max.Y),
                MathUtils.MaxF(box0.Max.Z, box1.Max.Z));
            return new BoundingBox(min, max);
        }
    }
}
