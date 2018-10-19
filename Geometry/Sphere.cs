using RayTracer.LinearAlgebra;
using RayTracer.Materials;
using RayTracer.Mathy;

namespace RayTracer.Geometry {
    public class Sphere : Hitable {

        public Sphere() {
        }

        public Sphere(Vec3 center, float radius, Material material) {
            this.Center = center;
            this.Radius = radius;
            this.Material = material;
        }

        public Vec3 Center { get; set; }

        public float Radius { get; set; }

        public Material Material { get; set;}

        public bool GetBoundingBox(float t0, float t1, ref BoundingBox box) {
            var middleToCorner = new Vec3(this.Radius, this.Radius, this.Radius);
            if (this.Radius < 0) {
                middleToCorner *= -1;
            }
            box = new BoundingBox(Center - middleToCorner, Center + middleToCorner);
            return true;
        }

        // dot(rayDir, rayDir) * t^2 + 2*t*dot(rayDir, rayOrig-center) + dot(rayOrig-center, rayOrig-center) - R*R=0
        public bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record) {
            float a = ray.Direction.LengthSquared; // dot(rayDir, rayDir) 
            Vec3 temp = new Vec3();
            Vec3.Subtract(ray.Origin, this.Center, temp);
            float b = 2 * ray.Direction.Dot(temp);//2*dot(rayDir, rayOrig-center) 
            float c = temp.LengthSquared - this.Radius * this.Radius;//dot(rayOrig-center, rayOrig-center) - R*R

            if (!PolyUtils.HasRealRoot(a, b, c)) {
                // No hit
                return false;
            }
            // Check first root for hit
            float firstRoot = PolyUtils.FirstRoot(a, b, c);
            if (firstRoot > tMin && firstRoot < tMax) {
                record.T = firstRoot;
                record.Point = ray.PointAt(firstRoot);
                Vec3.Subtract(record.Point, this.Center, temp);
                record.Normal = temp / this.Radius;
                record.Material = this.Material;
                return true;
            }
            // Check second root for hit
            float secondRoot = PolyUtils.SecondRoot(a, b, c);
            if (secondRoot > tMin && secondRoot < tMax) {
                record.T = secondRoot;
                record.Point = ray.PointAt(secondRoot);
                Vec3.Subtract(record.Point, this.Center, temp);
                record.Normal = temp / this.Radius;
                record.Material = this.Material;
                return true;
            }

            return false;
        }
    }
}
