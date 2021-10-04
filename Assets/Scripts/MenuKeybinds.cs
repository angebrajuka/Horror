using UnityEngine;
using UnityEngine.UI;

public class MenuKeybinds : MonoBehaviour
{
    // hierarchy
    public GameObject prefab_button_keybind;

    public void Init()
    {
        int i=0;
        foreach(var pair in PlayerInput.keybinds)
        {
            var button = Instantiate(prefab_button_keybind, transform);
            var rectTransform = button.GetComponent<RectTransform>();
            var pos = rectTransform.anchoredPosition;
            pos.Set(0, -50*i);
            rectTransform.anchoredPosition = pos;
            button.transform.GetChild(1).GetComponent<ButtonKeybind>().Init(pair.Key);
            button.transform.GetChild(0).GetComponent<Text>().text = pair.Key;

            i ++;
        }
    }
}