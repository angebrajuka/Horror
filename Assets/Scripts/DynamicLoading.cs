using UnityEngine;
using System.Collections.Generic;

public class DynamicLoading : MonoBehaviour
{
    // hierarchy
    public GameObject prefab_chunk;
    public Transform chunks;

    public Queue<Chunk> unloadedChunks;
    public Dictionary<(int x, int y), Chunk> loadedChunks;
    public const int CHUNK_SIZE = 10;
    public Vector2Int prevPos, currPos;

    public void Init()
    {
        unloadedChunks = new Queue<Chunk>();
        loadedChunks = new Dictionary<(int x, int y), Chunk>();
        prevPos = new Vector2Int(0, 0);
        currPos = new Vector2Int(0, 0);
    }

    public void Load(int x, int y)
    {
        var pp = unloadedChunks.Count == 0 ? Instantiate(prefab_chunk, Vector3.zero, Quaternion.identity, chunks).GetComponent<Chunk>() : unloadedChunks.Dequeue();
        pp.gameObject.SetActive(true);
        Vector3 pos = pp.transform.position;
        pos.x = x*CHUNK_SIZE;
        pos.z = y*CHUNK_SIZE;
        pp.transform.position = pos;

        loadedChunks.Add((x, y), pp);
        pp.Init(true, true, true, true);
    }

    public void Unload(int x, int y)
    {
        if(!loadedChunks.ContainsKey((x, y))) return;

        Chunk chunk = loadedChunks[(x, y)];
        chunk.gameObject.SetActive(false);
        loadedChunks.Remove((x, y));
        unloadedChunks.Enqueue(chunk);
    }

    public void UnloadTooFar()
    {
        var toUnload = new LinkedList<(int x, int y)>();
        foreach(var chunk in loadedChunks)
        {
            if(Mathf.Abs(currPos.x - chunk.Key.x) > 2 || Mathf.Abs(currPos.y - chunk.Key.y) > 2)
            {
                toUnload.AddLast((chunk.Key.x, chunk.Key.y));
            }
        }
        foreach(var pos in toUnload)
        {
            Unload(pos.x, pos.y);
        }
    }

    public void LoadAll()
    {
        for(int x=currPos.x-1; x<=currPos.x+1; x++) for(int y=currPos.y-1; y<=currPos.y+1; y++)
        {
            if(!loadedChunks.ContainsKey((x, y)))
            {
                Load(x, y);
            }
        }
    }

    void Update()
    {
        Vector3 p = PlayerMovement.m_rigidbody.position;
        currPos.Set((int)Mathf.Floor(p.x/10f), (int)Mathf.Floor(p.z/10f));

        if(currPos != prevPos || loadedChunks.Count == 0)
        {
            UnloadTooFar();
            LoadAll();
        }

        prevPos.Set(currPos.x, currPos.y);
    }
}