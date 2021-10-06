using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    // hierarchy
    public GameObject prefab_knife;
    public int minCount, maxCount;
    public float minRange, maxRange;

    void Start()
    {
        int count = Random.Range(minCount, maxCount);
        for(int i=0; i<count; i++)
        {
            Instantiate(prefab_knife, transform.position + Vector3.up + Random.onUnitSphere * Random.Range(minRange, maxRange), Quaternion.identity, EnemySpawning.instance.transform_enemies);
        }
    }
}