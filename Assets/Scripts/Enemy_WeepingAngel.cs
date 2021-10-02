using UnityEngine;
using static Enemy;

public class Enemy_WeepingAngel : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        _Update(transform);

        if(Enemy.LineOfSight(transform))
        {
            // move towards player
        }
        else
        {
            // despawn timer? distance despawn?
        }
    }
}