using System;
using System.Collections.Generic;
using System.Linq;
using RayTracer.LinearAlgebra;

namespace RayTracer.Geometry {
    public class BVHNode : Hitable {
        public BVHNode() { }

        public BVHNode(List<Hitable> hitables, float time0, float time1, Random random) {
            int axis = (int) (3 * random.NextDouble());
            switch(axis) {
                case 0:
                    hitables.Sort((a,b) => BoxXCompare(a,b));
                    break;
                case 1:
                    hitables.Sort((a, b) => BoxYCompare(a, b));
                    break;
                case 2:
                    hitables.Sort((a, b) => BoxZCompare(a, b));
                    break;
            }
            if (hitables.Count == 1) {
                this.Left = this.Right = hitables[0];
            } else if (hitables.Count == 2) {
                this.Left = hitables[0];
                this.Right = hitables[1];
            } else {
                int total = hitables.Count;
                int leftCount = total / 2;
                Left = new BVHNode(hitables.GetRange(0, leftCount).ToList(), time0, time1, random);
                Right = new BVHNode(hitables.GetRange(leftCount, total - leftCount).ToList(), time0, time1, random);
            }
            BoundingBox leftBox = null;
            BoundingBox rightBox = null;
            if (!this.Left.GetBoundingBox(time0, time1, ref leftBox) || !this.Right.GetBoundingBox(time0, time1, ref rightBox)) {
                throw new ArgumentException("No bounding box in BVHNode constructor");
            }
            this.Box = BoundingBox.SurroundingBox(leftBox, rightBox);
        }

        public Hitable Left { get; set;}
        public Hitable Right { get; set;}
        public BoundingBox Box { get; set;}

        public bool GetBoundingBox(float t0, float t1, ref BoundingBox box) {
            box = this.Box;
            return true;
        }

        public bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record) {
            if (Box.Hit(ray, tMin, tMax)) {
                HitRecord leftRecord = new HitRecord();
                HitRecord rightRecord = new HitRecord();
                bool hitLeft = this.Left.Hit(ray, tMin, tMax, ref leftRecord);
                bool hitRight= this.Right.Hit(ray, tMin, tMax, ref rightRecord);
                if (hitLeft && hitRight) {
                    if (leftRecord.T < rightRecord.T) {
                        record = leftRecord;
                    } else {
                        record = rightRecord;
                    }
                    return true;
                } else if (hitLeft) {
                    record = leftRecord;
                    return true;
                } else if (hitRight) {
                    record = rightRecord;
                    return true;
                }
            }
            return false;
        }

        private int BoxXCompare(Hitable a, Hitable b) {
            BoundingBox aBox = null;
            BoundingBox bBox = null;
            if (!a.GetBoundingBox(0,0, ref aBox) || !b.GetBoundingBox(0,0, ref bBox)) {
                throw new ArgumentException("No bounding box in BVHNode comparer x");   
            }
            return aBox.Min.X < bBox.Min.X ? -1 : 1;
        }

        private int BoxYCompare(Hitable a, Hitable b) {
            BoundingBox aBox = null;
            BoundingBox bBox = null;
            if (!a.GetBoundingBox(0, 0, ref aBox) || !b.GetBoundingBox(0, 0, ref bBox)) {
                throw new ArgumentException("No bounding box in BVHNode comparer y");
            }
            return aBox.Min.Y < bBox.Min.Y ? -1 : 1;
        }

        private int BoxZCompare(Hitable a, Hitable b) {
            BoundingBox aBox = null;
            BoundingBox bBox = null;
            if (!a.GetBoundingBox(0, 0, ref aBox) || !b.GetBoundingBox(0, 0, ref bBox)) {
                throw new ArgumentException("No bounding box in BVHNode comparer z");
            }
            return aBox.Min.Z < bBox.Min.Z ? -1 : 1;
        }
    }
}
