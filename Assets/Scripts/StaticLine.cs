using UnityEngine;

public class StaticLine : MonoBehaviour
{
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        var pos = rect.localPosition;
        pos.y = Random.Range(0, Screen.height);
        rect.localPosition = pos;
    }
}