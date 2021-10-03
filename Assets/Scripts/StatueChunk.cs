using UnityEngine;

public class StatueChunk : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != gameObject.layer)
        {
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }
}