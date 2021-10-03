using UnityEngine;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;

    // hierarchy
    public Transform transform_enemies;
    public GameObject[] prefabs_enemies;
    public float minDistance, maxDistance;
    public float minDelay, maxDelay;
    public float timer; // also initial delay

    public LinkedList<SaveData.S_Enemy> enemies;

    public void Init()
    {
        instance = this;

        enemies = new LinkedList<SaveData.S_Enemy>();
    }

    Vector3 GetPosition()
    {
        Vector3 position = DynamicLoading.currPos;
        position += DynamicLoading.neighborPositions[Random.Range(0, DynamicLoading.neighborPositions.Length)];
        position.x += 0.5f;
        position.z += 0.5f;
        position *= DynamicLoading.CHUNK_SIZE;

        return position;
    }

    public void Spawn(Vector3 position, int index)
    {
        Instantiate(prefabs_enemies[index], position, Quaternion.identity, transform_enemies);
        enemies.AddLast(new SaveData.S_Enemy(position, index));
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = Random.Range(minDelay, maxDelay);
            Spawn(GetPosition(), Random.Range(0, prefabs_enemies.Length));
        }
    }
}