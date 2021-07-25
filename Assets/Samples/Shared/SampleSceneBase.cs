using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace AillieoUtils.Geometries.Sample
{
    public class SampleSceneBase : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            ConfigCamera();
        }

        protected void ConfigCamera()
        {
            Camera camera = Camera.main;
            if (camera == null)
            {
                throw new System.Exception();
            }
            camera.orthographic = true;
            camera.backgroundColor = Color.black;
            camera.orthographicSize = 20;
        }
    }
}
