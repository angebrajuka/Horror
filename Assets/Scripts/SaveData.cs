using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public SaveData()
    {

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