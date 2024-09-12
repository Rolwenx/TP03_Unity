using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPointsQuadratic; 
    public Transform[] controlPointsCubic;

    public int NUMPOINTS = 50;
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = NUMPOINTS;
    }

    void Update()
    {
        // if user gives 3 control points, we can draw curve
        if (controlPointsQuadratic.Length == 3)
        {
            DrawQuadraticBezierCurve();
        }

        // if user gives 4 control points, we can draw curve
        if (controlPointsCubic.Length == 4)
        {
            DrawCubicBezierCurve();
        }
    }


    void DrawQuadraticBezierCurve()
    {
        for (int i = 0; i < NUMPOINTS; i++)
        {
            float t = i / (float)(NUMPOINTS - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, controlPointsQuadratic[0].position, controlPointsQuadratic[1].position, controlPointsQuadratic[2].position);
            _lineRenderer.SetPosition(i, point);
        }
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    void DrawCubicBezierCurve()
    {
        for (int i = 0; i < NUMPOINTS; i++)
        {
            float t = i / (float)(NUMPOINTS - 1);
            Vector3 point = CalculateCubicBezierPoint(t, controlPointsCubic[0].position, controlPointsCubic[1].position, controlPointsCubic[2].position, controlPointsCubic[3].position);
            _lineRenderer.SetPosition(i, point);
        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
    }
}
