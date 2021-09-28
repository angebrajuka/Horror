using UnityEngine;

public class Init : MonoBehaviour
{
    // hierarchy
    public Transform player;
    public AudioManager audioManager;
    public MusicController musicController;

    void Start()
    {
        audioManager.Init();
        musicController.Init();
        player.GetComponent<PauseHandler>().Init();

        player.GetComponent<PlayerInput>().Init();
        player.GetComponent<PlayerMovement>().Init();

        PauseHandler.UnPause();

        Destroy(gameObject);
    }
}