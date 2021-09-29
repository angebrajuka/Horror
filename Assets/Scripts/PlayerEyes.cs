using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    const int layerMask = 1 << Layers.PLAYER;

    float timer = 0;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PlayerStats.RAYCAST_DISTANCE, ~layerMask, QueryTriggerInteraction.Collide))
        {
            if(hit.collider.gameObject.layer == Layers.ENEMY)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    timer = 1;
                    PlayerBloodUI.AddSplatter();
                }
            }
        }
    }
}