using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonKeybind : MonoBehaviour
{
    string keybind;
    byte selected = 0;

    public void Init(string keybind)
    {
        this.keybind = keybind;
        SetText(false);
    }

    void SetText(bool anyKey)
    {
        Text t = transform.GetChild(0).GetComponent<Text>();
        t.text = "<" + (anyKey ? "press any key" : ""+PlayerInput.keybinds[keybind]) + ">";
        var rect = GetComponent<RectTransform>();
        var size = rect.sizeDelta;
        size.x = t.preferredWidth;
        rect.sizeDelta = size;
        GetComponent<EventTrigger>().enabled = !anyKey;
        GetComponent<Button>().enabled = !anyKey;
    }

    void OnKeyPress(KeyCode key)
    {
        PlayerInput.keybinds[keybind] = key;
        PlayerInput.SaveKeybinds();
        SetText(false);
    }

    public void OnClick()
    {
        selected = 1;
        SetText(true);
    }

    void Update()
    {
        if(Input.anyKey && selected == 2)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                selected = 0;
                SetText(false);
            }
            else
            {
                for(int i=0; i<670; i++)
                {
                    if(Input.GetKeyDown((KeyCode)i))
                    {
                        OnKeyPress((KeyCode)i);
                        selected = 0;
                        break;
                    }
                }
            }
        }
        if(selected == 1 && !Input.GetKey(KeyCode.Mouse0)) selected = 2;
    }
}
