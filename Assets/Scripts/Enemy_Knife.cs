using UnityEngine;

public class Enemy_Knife : MonoBehaviour
{
    // hierarchy
    public float accelleration, maxSpeed;

    Vector3 originalPosition;
    Vector3 targetOffset;
    [HideInInspector] public bool towards;
    [HideInInspector] public float speed=1;

    void Start()
    {
        originalPosition = transform.position;
        var mesh = transform.GetChild(0);
        var angles = mesh.localEulerAngles;
        angles.x = Random.Range(0f, 360f);
        mesh.localEulerAngles = angles;
        targetOffset = Random.onUnitSphere*Random.Range(0, 0.6f);
        towards = true;
    }

    void Update()
    {
        var targetLook = PlayerMovement.m_rigidbody.position+Vector3.up+targetOffset;

        if(towards)
        {
            speed += accelleration;
            var pos = transform.position;
            pos = Vector3.Lerp(pos, targetLook, Mathf.Min(speed, maxSpeed)*Time.deltaTime);
            transform.position = pos;
        }
        else
        {
            transform.Translate(Vector3.forward*2*-Time.deltaTime);
        }

        if(!towards && Vector3.Distance(transform.position, targetLook) > 3)
        {
            towards = true;
        }

        transform.LookAt(targetLook);
    }
}