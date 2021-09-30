using UnityEngine;
using System.Collections.Generic;

public class DynamicLoading : MonoBehaviour
{
    // hierarchy
    public GameObject prefab_chunk;
    public Transform chunks;

    public static Queue<Chunk> unloadedChunks;
    public static Dictionary<(int x, int z), Chunk> loadedChunks;
    public const int CHUNK_SIZE = 10;
    public Vector3Int prevPos, currPos;

    public void Init()
    {
        unloadedChunks = new Queue<Chunk>();
        loadedChunks = new Dictionary<(int x, int z), Chunk>();
        prevPos = new Vector3Int(0, 0, 0);
        currPos = new Vector3Int(0, 0, 0);
    }

    static readonly (int x, int z)[] neighborPositions = new (int x, int z)[]{
        (0, 1), // north
        (1, 0), // east
        (0, -1), // south
        (-1, 0) // west
    };

    // chunk array is north, east, south, west
    public static Chunk[] GetNeighbors(int x, int z)
    {
        var neighbors = new Chunk[4];

        for(int i=0; i<4; i++)
        {
            var pos = (neighborPositions[i].x+x, neighborPositions[i].z+z);
            neighbors[i] = loadedChunks.ContainsKey(pos) ? loadedChunks[pos] : null;
        }

        return neighbors;
    }

    public void Load(int x, int z, bool deadEnd=false)
    {
        var pp = unloadedChunks.Count == 0 ? Instantiate(prefab_chunk, Vector3.zero, Quaternion.identity, chunks).GetComponent<Chunk>() : unloadedChunks.Dequeue();
        pp.gameObject.SetActive(true);

        Vector3 pos = pp.transform.position;
        pos.x = x*CHUNK_SIZE;
        pos.z = z*CHUNK_SIZE;
        pp.transform.position = pos;

        loadedChunks.Add((x, z), pp);

        var neighbors = GetNeighbors(x, z);
        bool[] bools = new bool[4];
        bool any = false;
        for(int i=0; i<4; i++)
        {
            if(neighbors[i] == null)
            {
                bools[i] = !deadEnd && (Random.value > 0.7f || (!any && i == 3));
                any = any || bools[i];
            }
            else
            {
                GameObject[] other = new GameObject[]{neighbors[i].colliderS, neighbors[i].colliderW, neighbors[i].colliderN, neighbors[i].colliderE};
                bools[i] = !other[i].activeSelf;
            }
        }
        pp.Init(bools[0], bools[1], bools[2], bools[3]);
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
        new Vector3Int( 0,  0,  0), // center
        new Vector3Int( 0,  0,  1), // N
        new Vector3Int( 1,  0,  0), // E
        new Vector3Int( 0,  0, -1), // S
        new Vector3Int(-1,  0,  0), // W
        new Vector3Int( 1,  0,  1), // NE
        new Vector3Int( 1,  0, -1), // SE
        new Vector3Int(-1,  0, -1), // SW
        new Vector3Int(-1,  0,  1)  // NW
    };

    public void LoadAll()
    {
        for(int i=0; i<loadOrder.Length; i++)
        {
            var pos = loadOrder[i]+currPos;
            if(!loadedChunks.ContainsKey((pos.x, pos.z)))
            {
                Load(pos.x, pos.z, i>=5 ? Random.value > 0.5f : false);
            }
        }
    }

    void Update()
    {
        Vector3 p = PlayerMovement.m_rigidbody.position;
        currPos.Set((int)Mathf.Floor(p.x/CHUNK_SIZE), 0, (int)Mathf.Floor(p.z/CHUNK_SIZE));

        if(currPos != prevPos || loadedChunks.Count == 0)
        {
            UnloadTooFar();
            LoadAll();
        }

        prevPos.Set(currPos.x, 0, currPos.z);
    }
}