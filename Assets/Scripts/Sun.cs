using UnityEngine;

public class Sun : MonoBehaviour
{
    // hierarchy
    public Light sun;
    public float maxIntensity;
    public float speed;

    void Update()
    {
        sun.intensity += Time.deltaTime*speed;
        if(sun.intensity > maxIntensity)
        {
            sun.intensity = maxIntensity;
            Destroy(this);
        }
    }
}