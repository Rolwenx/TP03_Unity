using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float dayLength = 120f;
    // for day
    [SerializeField] private float maxIntensity = 1f; 
    // for night
    [SerializeField] private float minIntensity = 0.2f; 
    public static DayNightCycle instance;

    // O is morning - 1 is night
    private float timeOfDay = 0f; 
    [SerializeField] private Light sun;
    [SerializeField] public float timeSpeed = 0.1f;

    void Start()
    {
        if (sun == null)
        {
            sun = GetComponent<Light>();
        }
    }

    void Update()
    {
        // as time progress, time increase
        timeOfDay += (Time.deltaTime / dayLength * timeSpeed);

        // if (timeOfDay >= 1f) => if we are past night so cycle starts back
        if (timeOfDay >= 1f) 
        {
            timeOfDay = 0f;
        }

        float sunAngle = timeOfDay * 360f - 90f;
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        float sunIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Cos(timeOfDay * 2 * Mathf.PI));
        sun.intensity = sunIntensity;
    }

}