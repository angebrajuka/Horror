using UnityEngine;

public class Enemy
{
    public Transform transform;
    public OnScreen onScreen;
    public float despawnDistance;

    public Enemy(Transform transform, OnScreen onScreen, float despawnDistance)
    {
        this.transform = transform;
        this.onScreen = onScreen;
        this.despawnDistance = despawnDistance;
    }

    float Distance
    {
        get { return Vector3.Distance(transform.position, PlayerMovement.m_rigidbody.position); }
    }

    public bool LineOfSight()
    {
        const int layermask = 1 << Layers.CORN;

        return !Physics.Raycast(transform.position, PlayerMovement.instance.t_camera.position-transform.position, Distance, layermask);
    }

    public void Update()
    {
        if(!onScreen.onScreen && Distance > despawnDistance)
        {
            MonoBehaviour.Destroy(transform.gameObject);
        }
    }
}