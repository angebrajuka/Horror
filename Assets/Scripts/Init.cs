using UnityEngine;

public class Init : MonoBehaviour
{
    // hierarchy
    public Transform player;
    public Transform canvas;
    public AudioManager audioManager;
    public MusicController musicController;

    void Start()
    {
        audioManager.Init();
        musicController.Init();
        player.GetComponent<PauseHandler>().Init();

        player.GetComponent<PlayerInput>().Init();
        player.GetComponent<PlayerMovement>().Init();

        canvas.GetComponent<PlayerBloodUI>().Init();

        PauseHandler.UnPause();

        Destroy(gameObject);
    }
}