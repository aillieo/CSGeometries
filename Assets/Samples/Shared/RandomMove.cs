using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AillieoUtils.GeometriesSample
{
    public class RandomMove : MonoBehaviour
    {
        public enum MoveMode
        {
            X,
            Y,
            Z,
            XY,
            XZ,
            XYZ = 0,
        }

        public MoveMode mode;
        public float speed;
        public float range;

        private Vector3 center;

        private void OnEnable()
        {
            center = this.transform.localPosition;
        }

        private void Update()
        {

        }
    }
}
