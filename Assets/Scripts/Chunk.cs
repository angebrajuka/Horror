using UnityEngine;

public class Chunk : MonoBehaviour
{
    // hierarchy
    public GameObject[] colliders;

    public void Init(bool N, bool E, bool S, bool W)
    {
        colliders[0].SetActive(!N);
        colliders[1].SetActive(!E);
        colliders[2].SetActive(!S);
        colliders[3].SetActive(!W);
    }
}