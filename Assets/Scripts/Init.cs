using UnityEngine;

public class Init : MonoBehaviour
{
    // hierarchy
    public Transform player;
    public AudioManager audioManager;

    void Start()
    {
        audioManager.Init();
        player.GetComponent<PauseHandler>().Init();

        player.GetComponent<PlayerInput>().Init();
        player.GetComponent<PlayerMovement>().Init();

        PauseHandler.UnPause();

        Destroy(gameObject);
    }
}