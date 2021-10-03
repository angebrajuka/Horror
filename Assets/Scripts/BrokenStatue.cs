using UnityEngine;

public class BrokenStatue : MonoBehaviour
{
    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}