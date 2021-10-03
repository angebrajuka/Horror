using UnityEngine;

public class OnExit : MonoBehaviour
{
    void OnApplicationQuit()
    {
        SaveData.Save();
    }
}