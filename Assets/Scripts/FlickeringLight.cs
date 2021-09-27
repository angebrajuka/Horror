using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    // hierarchy
    public float radiusRange, intensityRange, timingAverage, timingRange, changeSpeed;
    public float averageRadius, averageIntensity;
    public bool useBeginningRadius;
    public bool useBeginningIntensity;

    private Light m_light;
    private float timer, interval;
    private float targetIntensity;
    private float targetRadius;

    void Start()
    {
        m_light = GetComponent<Light>();

        if(useBeginningRadius)
        {
            averageRadius = m_light.spotAngle;
        }
        if(useBeginningIntensity)
        {
            averageIntensity = m_light.intensity;
        }
        timer = 0;
        interval = 0;
        targetIntensity = averageIntensity;
        targetRadius = averageRadius;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            targetRadius = averageRadius + Random.value*radiusRange*2 - radiusRange;
            targetIntensity = averageIntensity + Random.value*intensityRange*2 - intensityRange;
            interval = timingAverage + Random.value*timingRange*2 - timingRange;
        }

        m_light.intensity = Mathf.Lerp(m_light.intensity, targetIntensity, changeSpeed);
        m_light.spotAngle = Mathf.Lerp(m_light.spotAngle, targetRadius, changeSpeed);
    }
}
