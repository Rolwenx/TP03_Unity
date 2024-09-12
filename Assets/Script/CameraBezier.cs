using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBezier : MonoBehaviour
{
    public Transform[] controlPointsCubic; 

    public Camera _mainCamera;  
    public float speed = 0.01f; 
    // to track the position on the curve (from 0 to 1)
    private float t = 0f;

    void Update()
    {
        if (controlPointsCubic.Length == 4)
        {
            Vector3 camera_position = CalculateCubicBezierPoint(t, controlPointsCubic[0].position, controlPointsCubic[1].position, controlPointsCubic[2].position, controlPointsCubic[3].position);
            
            _mainCamera.transform.position = camera_position;

            Vector3 nextPosition = CalculateCubicBezierPoint(t + 0.01f, controlPointsCubic[0].position, controlPointsCubic[1].position, controlPointsCubic[2].position, controlPointsCubic[3].position);
            _mainCamera.transform.LookAt(nextPosition);
            t += Time.deltaTime * speed;
            // if `t` exceeds 1, reset it to 0 for a continuous loop of camera and curve
            if (t > 1f)
            {
                t = 0f;
            }
        }
    }
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; 
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2; 
        point += ttt * p3; 

        return point;
    }
}
