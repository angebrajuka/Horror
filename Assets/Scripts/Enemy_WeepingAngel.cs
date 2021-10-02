using UnityEngine;

public class Enemy_WeepingAngel : MonoBehaviour
{
    // hierarchy
    public OnScreen onScreen;
    public float despawnDistance;

    public Enemy enemy;

    void Start()
    {
        enemy = new Enemy(transform, onScreen, despawnDistance);
    }

    void Update()
    {
        enemy.Update();

        if(enemy.LineOfSight() && !onScreen.onScreen)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerMovement.m_rigidbody.position, Time.deltaTime);
            transform.LookAt(PlayerMovement.m_rigidbody.position);
        }
    }
}