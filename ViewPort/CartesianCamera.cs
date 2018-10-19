using RayTracer.Geometry;
using RayTracer.LinearAlgebra;
using RayTracer.Mathy;
using System;

namespace RayTracer.ViewPort {
    public class CartesianCamera : Camera{
        private int rows;
        private int columns;
        private Vec3 u,v,w;
        float lensRadius;

        public CartesianCamera(int rows, int columns, float hFovDeg,
            Vec3 lookFrom, Vec3 lookAt, Vec3 lookUp,
            float aperture, float focusDist) {
            this.lensRadius = aperture / 2;

            float hFovRad = MathUtils.ToRad(hFovDeg);
            float halfWidth = (float)Math.Tan(hFovRad / 2);
            float aspectRatioInv = ((float)rows / columns);
            float halfHeight = aspectRatioInv * halfWidth;

            this.Origin = lookFrom;
            // Point from center of looking plane back at eye
            this.w = (lookFrom - lookAt).Normalized;
            // Create vector pointing to the right on plane with normal w seen from eye
            this.u = lookUp.Cross(w).Normalized;
            // create vector pointing up on plane with normal w seen from eye
            this.v = w.Cross(u);

            this.LowerLeftCorner = this.Origin - halfWidth * focusDist * u - halfHeight * focusDist * v - focusDist * w;
            this.Horizontal = 2 * halfWidth * focusDist * u;
            this.Vertical = 2 * halfHeight * focusDist * v;

            this.rows = rows;
            this.columns = columns;
        }

        public Vec3 Origin {get; set;}
        public Vec3 LowerLeftCorner {get; set; }
        public Vec3 Horizontal { get; set;}
        public Vec3 Vertical { get; set;}

        public Ray GetRay(int x, int y, Random random) {
            float u = (float)(x + random.NextDouble()) / this.columns;
            float v = (float)(y + random.NextDouble()) / this.rows;
            var randomDiskPoint = this.lensRadius * GeometrySampler.RandomPointInUnitDisk(random);
            var originOffset = this.u * randomDiskPoint.X + this.v * randomDiskPoint.Y;
            return new Ray(this.Origin + originOffset, this.LowerLeftCorner + u * this.Horizontal + v * this.Vertical - this.Origin - originOffset);
        }
    }
}
