using UnityEngine;
using UnityEngine.UI;

public class MenuMouse : MonoBehaviour
{
    public Slider sliderX, sliderY;

    public void Init()
    {
        sliderX.value = PlayerInput.speed_look.x / PlayerInput.MAX_LOOK_SPEED;
        sliderY.value = PlayerInput.speed_look.y / PlayerInput.MAX_LOOK_SPEED;
    }

    public void SliderMoved(bool x)
    {
        if(x) PlayerInput.speed_look.x = sliderX.value*PlayerInput.MAX_LOOK_SPEED;
        else  PlayerInput.speed_look.y = sliderY.value*PlayerInput.MAX_LOOK_SPEED;
        PlayerInput.SaveLookSpeed();
    }
}