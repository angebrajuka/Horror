using UnityEngine;

public class EnemyLookedAt : MonoBehaviour
{
    private float lookedAtTimer;
    private int lookedAt;

    public bool LookedAt
    {
        get { return lookedAtTimer > 0; }
    }
    public float LookedAtTimer
    {
        get { return lookedAtTimer; }
        set
        {
            lookedAtTimer = value;
            lookedAt = 2;
        }
    }


    void Update()
    {
        if(lookedAt > 0) lookedAt --;
        else lookedAtTimer = 0;
    }
}