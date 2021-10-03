using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public class S_Enemy
    {
        public int index;
        public float[] pos;

        public S_Enemy(Vector3 pos, int index)
        {
            this.pos = new float[]{pos.x, pos.y, pos.z};
            this.index = index;
        }
    }

    [System.Serializable]
    public class S_Chunk
    {
        public int[] pos;
        public bool[] nesw;

        public S_Chunk(Chunk chunk)
        {
            pos = new int[]{chunk.pos.x, chunk.pos.y, chunk.pos.z};

            nesw = new bool[chunk.colliders.Length];
            for(int i=0; i<nesw.Length; i++)
            {
                nesw[i] = chunk.IsOpen(i);
            }
        }
    }

    float[] player_pos;
    float[] player_rot_rb;
    float[] player_rot_cam;

    public float    flashlight_time;
    public float    flashlight_onDelay;
    public bool     flashlight_on;

    public (float x, float y, int index)[] bloodUI_splatters;

    public S_Enemy[] enemies;
    public float enemy_spawnTimer;

    public S_Chunk[] chunks;

    public SaveData()
    {
        player_pos = new float[3];
        for(int j=0; j<3; j++) player_pos[j] = PlayerMovement.m_rigidbody.position[j];
        player_rot_rb = new float[3];
        for(int j=0; j<3; j++) player_rot_rb[j] = PlayerMovement.m_rigidbody.transform.localEulerAngles[j];
        player_rot_cam = new float[3];
        for(int j=0; j<3; j++) player_rot_cam[j] = PlayerMovement.instance.t_camera.localEulerAngles[j];

        flashlight_time = PlayerFlashlight.instance.time;
        flashlight_onDelay = PlayerFlashlight.instance.onDelay;
        flashlight_on = PlayerFlashlight.instance.on;

        bloodUI_splatters = new (float, float, int)[PlayerBloodUI.splatters.Count];
        int i=0;
        foreach(var splatter in PlayerBloodUI.splatters)
        {
            bloodUI_splatters[i] = (splatter.pos.x, splatter.pos.y, splatter.index);
            i ++;
        }

        enemies = new S_Enemy[EnemySpawning.instance.enemies.Count];
        i=0;
        foreach(var enemy in EnemySpawning.instance.enemies)
        {
            enemies[i] = enemy;
            i ++;
        }
        enemy_spawnTimer = EnemySpawning.instance.timer;

        chunks = new S_Chunk[DynamicLoading.loadedChunks.Count];
        i=0;
        foreach(var pair in DynamicLoading.loadedChunks)
        {
            chunks[i] = new S_Chunk(pair.Value);
            i ++;
        }
    }

    public void Load()
    {
        Vector3 pos = PlayerMovement.m_rigidbody.position;
        for(int i=0; i<3; i++) pos[i] = player_pos[i];
        PlayerMovement.m_rigidbody.position = pos;

        Vector3 rot = PlayerMovement.m_rigidbody.transform.localEulerAngles;
        for(int i=0; i<3; i++) rot[i] = player_rot_rb[i];
        PlayerMovement.m_rigidbody.transform.localEulerAngles = rot;

        rot = PlayerMovement.instance.t_camera.localEulerAngles;
        for(int i=0; i<3; i++) rot[i] = player_rot_cam[i];
        PlayerMovement.instance.t_camera.localEulerAngles = rot;

        PlayerFlashlight.instance.time = flashlight_time;
        PlayerFlashlight.instance.onDelay = flashlight_onDelay;
        PlayerFlashlight.instance.on = flashlight_on;
        if(flashlight_on)
        {
            PlayerFlashlight.instance.flashlight.gameObject.SetActive(true);
        }

        foreach(var splatter in bloodUI_splatters)
        {
            PlayerBloodUI.AddSplatter(new Vector2(splatter.x, splatter.y), splatter.index, false);
        }

        foreach(var enemy in enemies)
        {
            EnemySpawning.instance.Spawn(new Vector3(enemy.pos[0], enemy.pos[1], enemy.pos[2]), enemy.index);
        }
        EnemySpawning.instance.timer = enemy_spawnTimer;

        foreach(var chunk in chunks)
        {
            DynamicLoading.instance.LoadRaw(new Vector3Int(chunk.pos[0], chunk.pos[1], chunk.pos[2]), chunk.nesw);
        }
    }

    public static string DirectoryPath
    {
        get { return Application.persistentDataPath + "/savedata/"; }
    }

    public static string FilePath
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
            BinaryFormatter formatter = new BinaryFormatter();
            var stream = new FileStream(FilePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            data.Load();
            stream.Close();

            return true;
        }
        return false;
    }
}