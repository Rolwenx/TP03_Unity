using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float dayLength = 120f;
    [SerializeField] private float maxIntensity = 1f; 
    [SerializeField] private float minIntensity = 0f; 
    public static DayNightCycle instance;

    private float timeOfDay = 1f;
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
        // Update time of day
        timeOfDay += (Time.deltaTime / dayLength * timeSpeed);

        if (timeOfDay >= 1f) 
        {
            timeOfDay = 0f;
        }

        float sunAngle = timeOfDay * 360f - 90f;
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);


        float sunIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Clamp01(Mathf.Sin(timeOfDay * 2 * Mathf.PI)));
        sun.intensity = sunIntensity;
    }
}
