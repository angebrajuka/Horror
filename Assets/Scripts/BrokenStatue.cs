using UnityEngine;

public class BrokenStatue : MonoBehaviour
{
    // hierarchy
    public Enemy_WeepingAngel angel;

    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}