using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    public static StaticEffect instance;

    // hierarchy
    public GameObject prefab_static_line;
    public int count;
    public float countdown;

    public void Init()
    {
        instance = this;

        for(int i=0; i<count; i++)
        {
            Instantiate(prefab_static_line, transform);
        }
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            Application.Quit();
            Debug.Log("exit");
        }
    }
}