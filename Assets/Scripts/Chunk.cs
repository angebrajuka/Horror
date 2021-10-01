using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    // hierarchy
    public GameObject[] colliders;
    public GameObject prefab_corn;
    public int density;
    public float variation;
    public float minHeight, maxHeight;

    public bool deadEnd;
    public Vector3Int pos;
    public int totalOpenings;

    static readonly int[] opposites = new int[]{2, 3, 0, 1};

    public void Init(int x, int z, bool deadEnd)
    {
        pos = new Vector3Int(x, 0, z);
        this.deadEnd = deadEnd;
        totalOpenings = 0;

        foreach(var collider in colliders)
        {
            collider.SetActive(true);
        }

        for(int i=0; i<colliders.Length; i++)
        {
            var position = pos+DynamicLoading.neighborPositions[i];
            if(DynamicLoading.loadedChunks.ContainsKey((position.x, position.z)) &&
                DynamicLoading.loadedChunks[(position.x, position.z)].IsOpen(opposites[i]))
            {
                colliders[i].SetActive(false);
                totalOpenings ++;
            }
        }

        int openings = Random.Range(deadEnd ? 0 : 1, 4);
        for(int i=0; i<openings && totalOpenings<colliders.Length-1; i++)
        {
            int j = Random.Range(0, 4);
            while(IsOpen(j%4))
            {
                j++;
                if(j > 8) return;
            }
            colliders[j%4].SetActive(false);
            totalOpenings ++;
        }
    }

    public bool IsOpen(int i)
    {
        return !colliders[i].activeSelf;
    }

    public Chunk AddAllCorn()
    {
        for(int c=1; c<transform.childCount; c++) // c starts at 1 to skip plane
        {
            AddCorn(transform.GetChild(c).GetComponent<BoxCollider>());
        }

        return this;
    }

    public void AddCorn(BoxCollider collider)
    {
        for(int x=0; x<=density; x++) for(int z=0; z<=density; z++)
        {
            var corn = Instantiate(prefab_corn, Vector3.zero, Quaternion.identity, collider.transform);

            var pos = corn.transform.localPosition;
            pos.x = Math.Remap(x, 0, density, -collider.size.x/2, collider.size.x/2)+Random.Range(-variation, variation);
            pos.y = 0;
            pos.z = Math.Remap(z, 0, density, -collider.size.z/2, collider.size.z/2)+Random.Range(-variation, variation);
            corn.transform.localPosition = pos;
            
            var rotation = corn.transform.eulerAngles;
            rotation.y = Random.Range(0f, 360f);
            corn.transform.eulerAngles = rotation;

            var scale = corn.transform.localScale;
            scale.y = Random.Range(minHeight, maxHeight);
            corn.transform.localScale = scale;
        }
    }
}