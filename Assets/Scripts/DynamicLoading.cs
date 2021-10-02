using UnityEngine;
using System.Collections.Generic;

public class DynamicLoading : MonoBehaviour
{
    public static DynamicLoading instance;

    // hierarchy
    public GameObject prefab_chunk;
    public Transform chunks;

    public static Queue<Chunk> unloadedChunks;
    public static Dictionary<(int x, int z), Chunk> loadedChunks;
    public const int CHUNK_SIZE = 10;
    public static Vector3Int prevPos, currPos;

    public void Init()
    {
        instance = this;

        unloadedChunks = new Queue<Chunk>();
        loadedChunks = new Dictionary<(int x, int z), Chunk>();
        prevPos = new Vector3Int(0, 0, 0);
        currPos = new Vector3Int(0, 0, 0);
    }

    public static readonly Vector3Int[] neighborPositions = new Vector3Int[]{
        Math.N,
        Math.E,
        Math.S,
        Math.W
    };

    public void LoadNeighbors(Chunk chunk, int iteration)
    {
        for(int i=0; i<neighborPositions.Length; i++)
        {
            var pos = chunk.pos + neighborPositions[i];

            bool continued = false;
            if(chunk.IsOpen(i))
            {
                bool dead = (i != neighborPositions.Length-1 || continued) && chunk.branches > 1 && Random.value > 0.4f;
                Load(pos, dead, iteration-1);
                continued = continued || !dead;
            }
        }
    }

    public Chunk Load(Vector3Int pos, bool deadEnd, int iteration)
    {
        if(!loadedChunks.ContainsKey((pos.x, pos.z)))
        {
            var chunk = unloadedChunks.Count == 0 ? Instantiate(prefab_chunk, Vector3.zero, Quaternion.identity, chunks).GetComponent<Chunk>().AddAllCorn() : unloadedChunks.Dequeue();
            chunk.gameObject.SetActive(true);

            Vector3 position = chunk.transform.position;
            position.x = pos.x*CHUNK_SIZE;
            position.z = pos.z*CHUNK_SIZE;
            chunk.transform.position = position;

            loadedChunks.Add((pos.x, pos.z), chunk);
            chunk.Init(pos.x, pos.z, deadEnd);
        }

        if(iteration > 0) LoadNeighbors(loadedChunks[(pos.x, pos.z)], iteration);

        return loadedChunks[(pos.x, pos.z)];
    }

    public void Unload(int x, int z)
    {
        if(!loadedChunks.ContainsKey((x, z))) return;

        Chunk chunk = loadedChunks[(x, z)];
        chunk.gameObject.SetActive(false);
        loadedChunks.Remove((x, z));
        unloadedChunks.Enqueue(chunk);
    }

    public void UnloadTooFar()
    {
        var toUnload = new LinkedList<(int x, int z)>();
        foreach(var chunk in loadedChunks)
        {
            if(Mathf.Abs(currPos.x - chunk.Key.x) > 2 || Mathf.Abs(currPos.z - chunk.Key.z) > 2)
            {
                toUnload.AddLast((chunk.Key.x, chunk.Key.z));
            }
        }
        foreach(var pos in toUnload)
        {
            Unload(pos.x, pos.z);
        }
    }

    static readonly Vector3Int[] loadOrder = new Vector3Int[]{
        Vector3Int.zero, // center
        Math.N,
        Math.E,
        Math.S,
        Math.W,
        Math.NE,
        Math.SE,
        Math.SW,
        Math.NW
    };

    void Update()
    {
        Vector3 p = PlayerMovement.m_rigidbody.position;
        currPos.Set((int)Mathf.Floor(p.x/CHUNK_SIZE), 0, (int)Mathf.Floor(p.z/CHUNK_SIZE));

        if(currPos != prevPos || loadedChunks.Count == 0)
        {
            UnloadTooFar();
            Load(currPos, false, 1);
        }

        prevPos.Set(currPos.x, 0, currPos.z);
    }
}