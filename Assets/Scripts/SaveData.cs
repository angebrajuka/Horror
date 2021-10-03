using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public class Enemy
    {
        int index;
        float[] pos;

        public Enemy(Vector3 pos, int index)
        {
            this.pos = new float[]{pos.x, pos.y, pos.z};
            this.index = index;
        }
    }


    float[] movement_pos;

    public float    flashlight_time;
    public float    flashlight_onDelay;
    public bool     flashlight_on;

    public (float, float, int)[] bloodUI_splatters;

    public Enemy[] enemies;

    public SaveData()
    {
        movement_pos = new float[]{PlayerMovement.m_rigidbody.position.x, PlayerMovement.m_rigidbody.position.y, PlayerMovement.m_rigidbody.position.z};

        flashlight_time = PlayerFlashlight.instance.time;
        flashlight_onDelay = PlayerFlashlight.instance.onDelay;
        flashlight_on = PlayerFlashlight.instance.on;

        bloodUI_splatters = new (float, float, int)[PlayerBloodUI.splatters.Count];
        int i=0;
        foreach(var splatter in PlayerBloodUI.splatters)
        {
            bloodUI_splatters[i] = (splatter.pos.x, splatter.pos.y, splatter.index);
            i++;
        }

        enemies = EnemySpawning.instance.Enemies;

    }

    public void Load()
    {

    }

    static string DirectoryPath
    {
        get { return Application.persistentDataPath + "/savedata/"; }
    }

    static string FilePath
    {
        get { return DirectoryPath+"slot0.sav"; }
    }

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        Directory.CreateDirectory(DirectoryPath);
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool TryLoad()
    {
        if(File.Exists(FilePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(FilePath, FileMode.Open);

                SaveData data = formatter.Deserialize(stream) as SaveData;
                data.Load();
                stream.Close();

                return true;
            }
            catch
            {
                File.Delete(FilePath);
            }
        }
        return false;
    }
}