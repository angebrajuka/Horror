using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    const int layerMask = ~(1<<Layers.PLAYER);

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PlayerStats.RAYCAST_DISTANCE, layerMask, QueryTriggerInteraction.Collide))
        {
            var e = hit.collider.gameObject.GetComponent<EnemyLookedAt>();
            if(e != null)
            {
                e.LookedAtTimer += Time.deltaTime;
            }
        }
    }
}