using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    // hierarchy
    public Transform enemies;
    public GameObject[] prefabs_enemies;
    public float minDistance, maxDistance;
    public float minDelay, maxDelay;
    public float timer; // also initial delay

    Vector3 GetPosition()
    {
        Vector3 position = DynamicLoading.currPos;
        position += DynamicLoading.neighborPositions[Random.Range(0, DynamicLoading.neighborPositions.Length)];
        position.x += 0.5f;
        position.z += 0.5f;
        position *= DynamicLoading.CHUNK_SIZE;
        // Vector2 offset = Random.insideUnitCircle.normalized*Random.Range(minDistance, maxDistance);
        // position.x += offset.x;
        // position.z += offset.y;

        return position;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = Random.Range(minDelay, maxDelay);
            Instantiate(prefabs_enemies[Random.Range(0, prefabs_enemies.Length)], GetPosition(), Quaternion.identity, enemies);
        }
    }
}