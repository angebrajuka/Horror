using UnityEngine;

public class Init : MonoBehaviour
{
    // hierarchy
    public Transform player;
    public Transform canvas;
    public AudioManager audioManager;
    public MusicController musicController;
    public bool load;

    void Start()
    {
        audioManager.Init();
        musicController.Init();
        player.GetComponent<PauseHandler>().Init();

        player.GetComponent<DynamicLoading>().Init();
        player.GetComponent<PlayerInput>().Init();
        player.GetComponent<PlayerMovement>().Init();
        player.GetComponent<PlayerFlashlight>().Init();
        player.GetComponent<EnemySpawning>().Init();

        canvas.GetComponent<PlayerBloodUI>().Init();

        if(load)
        {
            SaveData.TryLoad();
        }
        
        PauseHandler.UnPause();

        Destroy(gameObject);
    }
}