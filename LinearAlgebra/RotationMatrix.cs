using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.LinearAlgebra {
    /// <summary>
    /// If the 3D space is right-handed, this rotation will be counterclockwise when u points towards the observer (Right-hand rule). 
    /// Rotations in the counterclockwise direction are considered positive rotations.
    /// See https://en.wikipedia.org/wiki/Rotation_matrix#Rotation_matrix_from_axis_and_angle
    /// </summary>
    public class RotationMatrix {
        private float[,] matrix = new float[3,3];
        private Vec3 axis;
        private float angle;

        public RotationMatrix(Vec3 axis, float angle) {
            this.axis = new Vec3(axis.Normalized);
            this.angle = angle;
            ComputeMatrix();
        }

        private void ComputeMatrix() {
            float cos = (float) Math.Cos(this.angle);
            float sin = (float) Math.Sin(this.angle);
            float x = axis.X; float y = axis.Y; float z = axis.Z;

            matrix[0, 0] = cos + x * x * (1 - cos);
            matrix[0, 1] = x * y * (1 - cos) - z * sin;
            matrix[0, 2] = x * z * (1 - cos) + y * sin;

            matrix[1, 0] = y * x * (1 - cos) + z * sin;
            matrix[1, 1] = cos + y * y * (1 - cos);
            matrix[1, 2] = y * z * (1 - cos) - x * sin;

            matrix[2, 0] = z * x * (1 - cos) - y * sin;
            matrix[2, 1] = z * y * (1 - cos) + x * sin;
            matrix[2, 2] = cos + z * z * (1 - cos);
        }
        
        public Vec3 Rotate(Vec3 vec) {
            var rotated = new Vec3();
            for (int row = 0; row < 3; row++) {
                rotated.X += this.matrix[row, 0] * vec.X;
                rotated.Y += this.matrix[row, 1] * vec.Y;
                rotated.Z += this.matrix[row, 2] * vec.Z;
            }
            return rotated;
        }
    }
}
