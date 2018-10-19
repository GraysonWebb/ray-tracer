using System.Collections.Generic;
using RayTracer.LinearAlgebra;

namespace RayTracer.Geometry {
    public class HitableList : Hitable {
        public HitableList() {
            this.Hitables = new List<Hitable>();
        }

        public HitableList(List<Hitable> hitables) {
            this.Hitables = hitables;
        }

        public List<Hitable> Hitables { get; set; }

        public bool GetBoundingBox(float t0, float t1, ref BoundingBox box) {
            if (this.Hitables.Count == 0) {
                return false;
            }
            BoundingBox tempBox = null;
            bool firstTrue = this.Hitables[0].GetBoundingBox(t0, t1, ref tempBox);
            if (firstTrue) {
                return false;
            } else {
                box = tempBox;
            }
            for (int i = 1; i < this.Hitables.Count; i++) {
                if (this.Hitables[i].GetBoundingBox(t0, t1, ref tempBox)) {
                    box = BoundingBox.SurroundingBox(box, tempBox);
                } else {
                    return false;
                }
            }
            return true;
        }

        public bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record) {
            var tempRecord = new HitRecord();
            bool hitAnything = false;
            float closestYet = tMax;
            foreach(var hitable in this.Hitables) {
                if (hitable.Hit(ray, tMin, closestYet, ref tempRecord)) {
                    hitAnything = true;
                    closestYet = tempRecord.T;
                    record.SetFrom(tempRecord);
                }
            }
            return hitAnything;
        }
    }
}
