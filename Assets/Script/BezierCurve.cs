using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPointsQuadratic; // Pour la courbe quadratique (3 points de contrôle)
    public Transform[] controlPointsCubic;     // Pour la courbe cubique (4 points de contrôle)

    public int numPoints = 50; // Nombre de points de la courbe
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        // Tracer la courbe quadratique si trois points de contrôle sont fournis
        if (controlPointsQuadratic.Length == 3)
        {
            DrawQuadraticBezierCurve();
        }

        // Tracer la courbe cubique si quatre points de contrôle sont fournis
        if (controlPointsCubic.Length == 4)
        {
            DrawCubicBezierCurve();
        }
    }

    // Fonction pour tracer une courbe de Bézier quadratique
    void DrawQuadraticBezierCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, controlPointsQuadratic[0].position, controlPointsQuadratic[1].position, controlPointsQuadratic[2].position);
            lineRenderer.SetPosition(i, point);
        }
    }

    // Fonction pour calculer un point sur la courbe quadratique
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    // Fonction pour tracer une courbe de Bézier cubique
    void DrawCubicBezierCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateCubicBezierPoint(t, controlPointsCubic[0].position, controlPointsCubic[1].position, controlPointsCubic[2].position, controlPointsCubic[3].position);
            lineRenderer.SetPosition(i, point);
        }
    }

    // Fonction pour calculer un point sur la courbe cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
    }
}
