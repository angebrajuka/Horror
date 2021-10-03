using UnityEngine;

public class OnExit : MonoBehaviour
{
    void OnApplicationQuit()
    {
        if(!PlayerLife.dead)
        {
            SaveData.Save();
        }
    }
}