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

    public Dictionary<int, GameObject> dict_prefabs_enemies;
    public static SaveData.S_Enemy[] enemies
    {
        get
        {
            var e = new SaveData.S_Enemy[instance.transform_enemies.childCount];
            for(int i=0; i<e.Length; i++)
            {
                var t = instance.transform_enemies.GetChild(i);
                e[i] = new SaveData.S_Enemy(t.position, t.gameObject.GetComponent<Int>().value);
            }
            return e;
        }
    }

    public void Init()
    {
        instance = this;

        dict_prefabs_enemies = new Dictionary<int, GameObject>();
        foreach(var prefab in prefabs_enemies)
        {
            dict_prefabs_enemies.Add(prefab.GetComponent<Int>().value, prefab);
        }
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
        var e = Instantiate(dict_prefabs_enemies[index], position, Quaternion.identity, transform_enemies);
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