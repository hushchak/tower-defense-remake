using System;
using UnityEditor;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private bool gizmos;

    public Transform StartPoint => points[0];
    public int PointsCount => points.Length;

    internal Transform GetPoint(int point)
    {
        if (point < 0 || point > PointsCount - 1)
            return null;
        return points[point];
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        if (points != null && points.Length > 0)
        {
            GUIStyle style = new();
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            for (int i = 0; i < points.Length; i++)
            {
                Handles.Label(points[i].transform.position + Vector3.up * 0.7f, points[i].name, style);
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
            }
        }
    }
#endif
}
