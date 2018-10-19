using RayTracer.LinearAlgebra;
using RayTracer.Materials;

namespace RayTracer.Geometry {
    public class HitRecord {
        public float T;
        public Vec3 Point;
        public Vec3 Normal;
        public Material Material;

        public HitRecord(float t = -1, Vec3 hitPoint = null, Vec3 normal = null, Material material = null) {
            this.T = t;
            this.Point = hitPoint;
            this.Normal = normal;
            this.Material = material;
        }

        public void SetFrom(HitRecord record) {
            this.T = record.T;
            this.Point = record.Point;
            this.Normal = record.Normal;
            this.Material = record.Material;
        }
    }
    public interface Hitable {
        bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record);
        bool GetBoundingBox(float t0, float t1, ref BoundingBox box);
    }
}
