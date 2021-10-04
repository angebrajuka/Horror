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
    public bool fadeIn;

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

        if(fadeIn)
        {
            m_light.intensity = 0;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            targetRadius = averageRadius + Random.Range(-radiusRange, radiusRange);
            targetIntensity = averageIntensity + Random.Range(-intensityRange, intensityRange);
            interval = timingAverage + Random.Range(-timingRange, timingRange);
        }

        m_light.intensity = Mathf.Lerp(m_light.intensity, targetIntensity, changeSpeed*Time.deltaTime);
        m_light.spotAngle = Mathf.Lerp(m_light.spotAngle, targetRadius, changeSpeed*Time.deltaTime);
    }
}
