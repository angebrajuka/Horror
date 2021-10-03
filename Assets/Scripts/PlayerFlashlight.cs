using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public static PlayerFlashlight instance;

    // hierarchy
    public FlickeringLight flashlight;
    public AudioClip click_on;
    public AudioClip click_off;
    public float time;
    public float onDelay;
    [HideInInspector] public bool on;

    public void Init()
    {
        instance = this;

        on = false;
    }

    void Update()
    {
        if(!on)
        {
            onDelay -= Time.deltaTime;
            if(onDelay <= 0)
            {
                flashlight.gameObject.SetActive(true);
                AudioManager.PlayClip(click_on);
                on = true;
            }
        }

        time -= Time.deltaTime;
        if(time <= 0)
        {
            if(flashlight.intensityRange != 0)
            {
                AudioManager.PlayClip(click_off);
                flashlight.intensityRange = 0;
            }
            flashlight.averageIntensity -= Time.deltaTime;
        }
    }
}