using UnityEngine;

public class Enemy_WeepingAngel : MonoBehaviour
{
    // hierarchy
    public MeshRenderer[] eyes;
    public OnScreen onScreen;
    public EnemyLookedAt enemyLookedAt;
    public GameObject mesh_default, mesh_explode;
    public GameObject prefab_innerDemon;
    public float despawnDistance;
    public float breakTimerMax;
    public float moveSpeed;

    public Enemy enemy;

    void Start()
    {
        enemy = new Enemy(transform, onScreen, despawnDistance);
    }

    void Update()
    {
        enemy.OnUpdate();

        if(enemy.LineOfSight() && !onScreen.onScreen)
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