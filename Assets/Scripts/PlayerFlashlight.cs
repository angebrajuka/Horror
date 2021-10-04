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
    [HideInInspector] public bool on
    {
        get
        {
            return onDelay <= 0 && time > 0;
        }
    }

    private float intensityAvg, intensityRange;

    public void Init()
    {
        instance = this;

        intensityAvg = flashlight.averageIntensity;
        intensityRange = flashlight.intensityRange;

        flashlight.averageIntensity = 0;
        flashlight.intensityRange = 0;
    }

    public void InstantOn()
    {
        flashlight.averageIntensity = intensityAvg;
        flashlight.intensityRange = intensityRange;
    }

    public void ClickOn()
    {
        InstantOn();
        AudioManager.PlayClip(click_on);
    }

    public void ClickOff()
    {
        flashlight.changeSpeed = 1f;
        flashlight.intensityRange = 0;
        flashlight.averageIntensity = 0;
        AudioManager.PlayClip(click_off);
    }

    void Update()
    {
        if(onDelay > 0)
        {
            onDelay -= Time.deltaTime;
            if(onDelay <= 0)
            {
                ClickOn();
            }
        }

        if(time > 0)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                ClickOff();
            }
        }
    }
}