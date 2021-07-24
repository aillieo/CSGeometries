using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AillieoUtils.Geometries.Sample
{
    public class RandomMove2D : MonoBehaviour
    {
        public MoveMode mode;
        public float speed;

        // 矩形模式
        public Vector2 max;
        public Vector2 min;

        // 圆形模式
        public Vector2 center;
        public float range;

        public enum MoveMode
        {
            InCircle,
            InRect,
        }

        private Vector3 target;

        private void OnEnable()
        {
            ResetTarget();
        }

        private void ResetTarget()
        {
            switch (mode)
            {
                case MoveMode.InCircle:
                    target = (Random.insideUnitCircle * range + center).ToVector3();
                    break;
                case MoveMode.InRect:
                    target = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)).ToVector3();
                    break;
            }
        }

        private void Update()
        {
            if (Vector3.SqrMagnitude(transform.position - target) < 0.001f)
            {
                ResetTarget();
                return;
            }

            this.transform.localPosition = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * speed);
        }
    }
}
