using UnityEngine;

public class Enemy_WeepingAngel : Enemy
{
    // hierarchy
    public AudioClip[] behindYou;
    public MeshRenderer[] eyes;
    public EnemyLookedAt enemyLookedAt;
    public GameObject mesh_default, mesh_explode;
    public GameObject prefab_innerDemon;
    public float breakTimerMax;
    public float moveSpeed;
    public float killDistance;

    void Start()
    {
    }

    void Update()
    {
        OnUpdate();

        if(LineOfSight() && !onScreen.onScreen && Vector3.Distance(transform.position, PlayerMovement.m_rigidbody.position) > killDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerMovement.m_rigidbody.position, Time.deltaTime*moveSpeed);
            transform.LookAt(PlayerMovement.m_rigidbody.position);
            if(Vector3.Distance(transform.position, PlayerMovement.m_rigidbody.position) <= killDistance)
            {
                PlayerInput.frozen = true;
                AudioManager.PlayClip(behindYou[Random.Range(0, behindYou.Length-1)]);
            }
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