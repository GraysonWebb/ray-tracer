using RayTracer.LinearAlgebra;
using RayTracer.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Geometry {
    public class Rectangle : Hitable{
        private Vec3 u;
        private float width;
        private Vec3 v;
        private float height;
        private Vec3 origin;
        private Vec3 originRecBase;
        private Vec3 normal;

        private float x0;
        private float x1;
        private float y0;
        private float y1;
        private float z;
        
        public Rectangle() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"> The origin of the rectangle, from which u and v extrude.</param>
        /// <param name="u"> First side of rectangle.</param>
        /// <param name="v"> Second side of rectangle</param>
        /// <remarks>
        /// U and V must be orthogonal.
        /// The normal to this rectangle is given by normalized(cross(u,v)).
        /// </remarks>
        public Rectangle(Vec3 origin, Vec3 u, Vec3 v, Material material) {
            if (Math.Abs(u.Dot(v)) > 1e-6) {
                throw new ArgumentException("Error in rectangle constructor, u and v are not orthogonal");
            }
            this.Material = material;

            this.width = u.Length;
            this.u = u.Normalized;
            
            this.height = v.Length;
            this.v = v.Normalized;
            
            this.normal = u.Cross(v).Normalized;

            this.origin = origin;
            this.originRecBase = TransformUtils.OrthogonalBaseChange(origin, this.u, this.v, this.normal);

            this.x0 = this.originRecBase.X;
            this.x1 = x0 + this.width;

            this.y0 = this.originRecBase.Y;
            this.y1 = y0 + this.height;

            this.z = this.originRecBase.Z;
        }

        public Material Material { get; set; }

        public bool GetBoundingBox(float t0, float t1, ref BoundingBox box) {
            float epsilon = 0.0001f;
            box = new BoundingBox(this.origin - epsilon * this.normal, 
                this.origin + this.width*u + this.height*v + epsilon * this.normal);
            return true;
        }
        public bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record) {
            var originRecBase = TransformUtils.OrthogonalBaseChange(ray.Origin, this.u, this.v, this.normal);
            var dirRecBase = TransformUtils.OrthogonalBaseChange(ray.Direction, this.u, this.v, this.normal);
            
            float t = (this.z - originRecBase.Z) / dirRecBase.Z;
            if (t < tMin || t > tMax) {
                return false;
            }
            float x = originRecBase.X + t * dirRecBase.X;
            float y = originRecBase.Y + t * dirRecBase.Y;
            if (x < this.x0 || x > this.x1 || y < this.y0 || y > this.y1) {
                return false;
            }
            record.T = t;
            record.Material = this.Material;
            record.Point = ray.PointAt(t);
            record.Normal = this.normal;
            return true;
        }
    }
}
