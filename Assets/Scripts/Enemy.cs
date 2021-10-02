using UnityEngine;

public static class Enemy
{

    public static bool LineOfSight(Transform enemy)
    {
        const int layermask = 1 << Layers.CORN;

        return !Physics.Raycast(enemy.position, PlayerMovement.m_rigidbody.position-enemy.position, Vector3.Distance(enemy.position, PlayerMovement.m_rigidbody.position), layermask);
    }

    public static void _Update(Transform enemy)
    {
        
    }
}