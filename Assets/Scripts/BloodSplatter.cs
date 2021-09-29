using UnityEngine;
using UnityEngine.UI;

public class BloodSplatter : MonoBehaviour
{
    // hierarchy
    public float speed;

    // components
    RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        Color c = image.color;
        c.a += Time.deltaTime*speed;
        image.color = c;
        if(image.color.a >= 1)
        {
            Destroy(this);
        }
    }
}