using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AillieoUtils.Geometries.Sample
{
    public class RandomMove2D : MonoBehaviour
    {
        public MoveMode mode;
        public float speed;

        // 圆形模式
        // 环模式
        public Vector2 center;
        public float range0;

        // 矩形模式
        // 线模式
        public Vector2 p0;
        public Vector2 p1;

        // 环模式
        public float range1;

        public enum MoveMode
        {
            InRect,
            InCircle,
            Line,
            Ring,
        }

        private Vector3 target;
        private Vector2 polarParams;

        private void OnEnable()
        {
            ResetTarget();
        }

        private void ResetTarget()
        {
            switch (mode)
            {
                case MoveMode.InCircle:
                    target = (Random.insideUnitCircle * range0 + center).ToVector3();
                    break;
                case MoveMode.InRect:
                    target = new Vector2(Random.Range(p0.x, p1.x), Random.Range(p0.y, p1.y)).ToVector3();
                    break;
                case MoveMode.Line:
                    target = Vector2.Lerp(p0, p1, Random.Range(0f, 1f)).ToVector3();
                    break;
                case MoveMode.Ring:
                    target = new Vector3(Random.Range(range0, range1), Random.Range(-360f, 360f), 0f);
                    break;
            }
        }

        private void Update()
        {
            switch (mode)
            {
                case MoveMode.InCircle:
                case MoveMode.InRect:
                case MoveMode.Line:
                    if (Vector3.SqrMagnitude(transform.localPosition - target) < speed * Time.deltaTime)
                    {
                        ResetTarget();
                    }
                    else
                    {
                        transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, Time.deltaTime * speed);
                    }
                    break;

                case MoveMode.Ring:
                    if (Vector2.SqrMagnitude(polarParams - (Vector2)target) < speed * Time.deltaTime)
                    {
                        ResetTarget();
                    }
                    else
                    {
                        polarParams = Vector2.MoveTowards(polarParams, (Vector2)target, speed * Time.deltaTime);
                        Vector2 targetPos2d = center + polarParams.x * new Vector2(Mathf.Cos(polarParams.y), Mathf.Sin(polarParams.y));
                        transform.localPosition = targetPos2d.ToVector3();
                    }
                    break;
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(RandomMove2D))]
    [CanEditMultipleObjects]
    public class RandomMove2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RandomMove2D randomMove2D = target as RandomMove2D;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.mode)));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.speed)), new GUIContent("Speed"));

            EditorGUILayout.BeginVertical("box");

            switch (randomMove2D.mode)
            {
                case RandomMove2D.MoveMode.InCircle:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.center)), new GUIContent("Center"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.range0)), new GUIContent("Radius"));
                    break;
                case RandomMove2D.MoveMode.InRect:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.p0)), new GUIContent("LeftBottom"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.p1)), new GUIContent("RightTop"));
                    break;
                case RandomMove2D.MoveMode.Line:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.p0)), new GUIContent("P0"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.p1)), new GUIContent("P1"));
                    break;
                case RandomMove2D.MoveMode.Ring:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.center)), new GUIContent("Center"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.range0)), new GUIContent("InnerRadius"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(randomMove2D.range1)), new GUIContent("OuterRadius"));
                    break;
            }

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Set Current As Center"))
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    foreach (var t in targets)
                    {
                        SerializedObject so = new SerializedObject(t);
                        SetCurretAsCenter(t, so);
                        so.ApplyModifiedProperties();
                    }
                }
                else
                {
                    SetCurretAsCenter(target, serializedObject);
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }

        private static void SetCurretAsCenter(Object target, SerializedObject serializedObject)
        {
            RandomMove2D randomMove2D = target as RandomMove2D;
            Vector2 position2d = randomMove2D.transform.localPosition.ToVector2();
            switch (randomMove2D.mode)
            {
                case RandomMove2D.MoveMode.InCircle:
                case RandomMove2D.MoveMode.Ring:
                    serializedObject.FindProperty(nameof(randomMove2D.center)).vector2Value = position2d;
                    break;
                case RandomMove2D.MoveMode.InRect:
                case RandomMove2D.MoveMode.Line:
                    Vector2 range = (randomMove2D.p1 - randomMove2D.p0);
                    Vector2 p0 = position2d - range / 2;
                    Vector2 p1 = position2d + range / 2;
                    serializedObject.FindProperty(nameof(randomMove2D.p0)).vector2Value = p0;
                    serializedObject.FindProperty(nameof(randomMove2D.p1)).vector2Value = p1;
                    break;
            }
        }
    }

#endif

}
