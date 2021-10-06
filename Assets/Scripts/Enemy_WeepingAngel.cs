using UnityEngine;

public class Enemy_WeepingAngel : Enemy
{
    // hierarchy
    public MeshRenderer[] eyes;
    public EnemyLookedAt enemyLookedAt;
    public GameObject mesh_default, mesh_explode;
    public GameObject prefab_innerDemon;
    public float breakTimerMax;
    public float moveSpeed;

    void Start()
    {
    }

    void Update()
    {
        OnUpdate();

        if(LineOfSight() && !onScreen.onScreen)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerMovement.m_rigidbody.position, Time.deltaTime*moveSpeed);
            transform.LookAt(PlayerMovement.m_rigidbody.position);
        }

        foreach(var eye in eyes)
        {
            eye.material.SetFloat("e", enemyLookedAt.LookedAtTimer / breakTimerMax);
        }

        if(enemyLookedAt.LookedAtTimer > breakTimerMax)
        {
            enabled = false;
            mesh_default.SetActive(false);
            mesh_explode.SetActive(true);
            Instantiate(prefab_innerDemon, transform.position, Quaternion.identity);
        }
    }
}