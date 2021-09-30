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
    public float density;
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
        for(float x=-collider.size.x/2; x<collider.size.x/2; x+=density) for(float z=-collider.size.z/2; z<collider.size.z/2; z+=density)
        {
            var corn = disabledCorns.Count == 0 ? Instantiate(prefab_corn, Vector3.zero, Quaternion.identity) : disabledCorns.Dequeue();
            corn.transform.SetParent(collider.transform);
            
            var pos = corn.transform.localPosition;
            pos.x = x+Random.Range(-variation, variation);
            pos.y = 0;
            pos.z = z+Random.Range(-variation, variation);
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