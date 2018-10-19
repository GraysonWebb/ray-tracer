using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.LinearAlgebra;
using RayTracer.Mathy;

namespace RayTracer.ViewPort {
    public class RadialCamera : Camera {
        private const float pi = (float) Math.PI;
        
        private int rows;
        private int columns;

        private float hFovStart;
        private float hRadStep;

        private float vFovStart;
        private float vRadStep;

        private Vec3 originToCenter;
        private Vec3 vVec;
        private Vec3 hVec;
        Random rand;


        public RadialCamera(int rows, int columns, float hFov, Vec3 lookFrom, Vec3 lookAt, Vec3 lookUp) {
            this.rows = rows;
            this.columns = columns;
            this.rand = new Random(1338);

            this.LookFrom = lookFrom;
            this.LookAt = lookAt;
            this.originToCenter = this.LookAt - this.LookFrom;

            // Point from center of looking plane back at eye
            var w = (lookFrom - lookAt).Normalized;
            // Create vector pointing to the right on plane with normal w seen from eye
            var u = lookUp.Cross(w).Normalized;
            // create vector pointing up on plane with normal w seen from eye
            var v = w.Cross(u);
            // Todo: do this better.
            this.hVec = u;
            this.vVec = v;
            
            
            this.HFovDeg = hFov;
            this.HFovRad = MathUtils.ToRad(this.HFovDeg);

            this.hFovStart = this.HFovRad * -0.5f;
            this.hRadStep = this.HFovRad / (columns - 1);

            // If image is 200*100 with a hFov of 120 then rows/columns=0.5 and 
            // vertical fov should be 60;
            float aspectRatioInv = ((float)rows / columns);
            this.VFovDeg = this.HFovDeg * aspectRatioInv; // (height / width)
            this.VFovRad = MathUtils.ToRad(this.VFovDeg);
            //this.VFovRad = ((float)columns/ rows) * this.hRadStep * rows;
            //this.VFovDeg = MathUtils.ToDeg(this.VFovRad);

            this.vFovStart = this.VFovRad * -0.5f;
            this.vRadStep = this.VFovRad / (rows - 1);
        }


        public Vec3 LookFrom { get; }

        public Vec3 LookAt { get; }

        public float HFovRad { get; }

        public float HFovDeg { get; }

        public float VFovRad { get; }

        public float VFovDeg { get; }

        public Ray GetRay(int x, int y, Random random) {
            float u = (float)(x + random.NextDouble());
            float v = (float)(y + random.NextDouble());
            float hRad = this.hFovStart + u * this.hRadStep;
            float vRad = this.vFovStart + v * this.vRadStep;

            var hRotMat = new RotationMatrix(this.hVec, vRad);
            var direction = hRotMat.Rotate(this.originToCenter);

            var perpAxis = hRotMat.Rotate(this.vVec);
            var perpRotMat = new RotationMatrix(perpAxis, -hRad);
            direction = perpRotMat.Rotate(direction);

            return new Ray(this.LookFrom, direction);
        }
    }
}
