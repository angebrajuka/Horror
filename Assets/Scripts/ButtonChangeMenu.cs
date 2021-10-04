using UnityEngine;

public class ButtonChangeMenu : MonoBehaviour
{
    // hierarchy
    public int menu;

    public void OnClick()
    {
        MenuHandler.CurrentMenu = menu;
    }
}