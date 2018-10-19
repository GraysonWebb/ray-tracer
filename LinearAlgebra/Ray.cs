namespace RayTracer.LinearAlgebra {
    public class Ray {
        
        public Ray() {
        }

        public Ray(Vec3 origin, Vec3 direction) {
            this.Origin = origin;
            this.Direction = direction;
            this.InvertedDirection = 1 / direction;
            this.InvertedDirectionSign[0] = (InvertedDirection.X < 0) ? 1 : 0;
            this.InvertedDirectionSign[1] = (InvertedDirection.Y < 0) ? 1 : 0;
            this.InvertedDirectionSign[2] = (InvertedDirection.Z < 0) ? 1 : 0;
        }

        public Vec3 Origin { get; set;}
        public Vec3 Direction { get; set;}
        public Vec3 InvertedDirection { get; }
        /// <summary>
        /// 1 if Direction[i] < 0, corresponding to max in binding box, 
        /// otherwise 0, corresponding to min
        /// </summary>
        public int[] InvertedDirectionSign { get; } = new int[3];

        public Vec3 PointAt(float t) {
            return Origin + t * Direction;
        }
    }
}
