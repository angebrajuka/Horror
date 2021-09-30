using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    // hierarchy
    public GameObject colliderN;
    public GameObject colliderE;
    public GameObject colliderS;
    public GameObject colliderW;
    public GameObject prefab_corn;
    public int density;
    public float variation;
    public float minHeight, maxHeight;

    Queue<GameObject> disabledCorns=new Queue<GameObject>();

    public void Init(bool N, bool E, bool S, bool W)
    {
        colliderN.SetActive(!N);
        colliderE.SetActive(!E);
        colliderS.SetActive(!S);
        colliderW.SetActive(!W);

        disabledCorns.Clear();

        for(int c=1; c<transform.childCount; c++) // c starts at 1 to skip plane
        {
            var collider = transform.GetChild(c);
            for(int i=0; i<collider.transform.childCount; i++)
            {
                var child = collider.transform.GetChild(i);
                child.gameObject.SetActive(false);
                disabledCorns.Enqueue(child.gameObject);
            }

            if(collider.gameObject.activeSelf)
            {
                AddCorn(collider.GetComponent<BoxCollider>());
            }
        }
    }

    public void AddCorn(BoxCollider collider)
    {
        for(int x=0; x<=density; x++) for(int z=0; z<=density; z++)
        {
            var corn = disabledCorns.Count == 0 ? Instantiate(prefab_corn, Vector3.zero, Quaternion.identity) : disabledCorns.Dequeue();
            corn.transform.SetParent(collider.transform);
            
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
            corn.SetActive(true);
        }
    }
}