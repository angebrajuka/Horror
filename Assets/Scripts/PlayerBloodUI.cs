using UnityEngine;
using UnityEngine.UI;

public class PlayerBloodUI : MonoBehaviour
{
    public static PlayerBloodUI instance;

    // hierarchy
    public GameObject prefab_splatter;

    static Texture2D[] splatters;
    static AudioClip[] clips;

    public void Init()
    {
        instance = this;

        splatters = Resources.LoadAll<Texture2D>("Sprites/BloodSplatters");
        clips = Resources.LoadAll<AudioClip>("Audio/BloodSplatterSounds");
    }

    public static void AddSplatter(Vector2 position=default(Vector2))
    {
        if(position == default(Vector2))
        {
            position = new Vector2(Random.value*Screen.width, Random.value*Screen.height);
        }

        var splatter = Instantiate(instance.prefab_splatter, position, Quaternion.identity, instance.transform);
        splatter.GetComponent<RawImage>().texture = splatters[Random.Range(0, splatters.Length)];
        AudioManager.PlayClip(clips[Random.Range(0, clips.Length)]);
    }
}