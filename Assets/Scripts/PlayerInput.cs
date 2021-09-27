using UnityEngine;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    // settings
    public static Dictionary<string, KeyCode> keybinds;
    public static Vector2 speed_look;

    // input
    public static Vector3 input_move;
    public static Vector2 input_look;

    public void Init()
    {
        instance = this;

        keybinds = new Dictionary<string, KeyCode>();

        input_move = new Vector3(0, 0);
        input_look = new Vector2(0, 0);
        speed_look = new Vector2(1.1f, 0.8f);

        DefaultKeybinds();
    }

    public static void LoadKeybinds()
    {

    }

    public static void SaveKeybinds()
    {

    }

    public static void DefaultKeybinds()
    {
        keybinds.Clear();
        keybinds.Add("walk_front",      KeyCode.W);
        keybinds.Add("walk_back",       KeyCode.S);
        keybinds.Add("walk_left",       KeyCode.A);
        keybinds.Add("walk_right",      KeyCode.D);
        keybinds.Add("jump",            KeyCode.Space);
    }

    public static bool GetKey(string key)
    {
        return Input.GetKey(keybinds[key]);
    }

    public static bool GetKeyDown(string key)
    {
        return Input.GetKeyDown(keybinds[key]);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseHandler.Pause();
        }

        // movement
        {
            input_move.Set(0, 0, 0);
            if(GetKey("walk_front"))    input_move.z ++;
            if(GetKey("walk_back"))     input_move.z --;
            if(GetKey("walk_left"))     input_move.x --;
            if(GetKey("walk_right"))    input_move.x ++;
            input_move.Normalize();

            if(GetKey("jump")) input_move.y = PlayerStats.JUMP_FORCE;
        }

        // mouse look
        {
            input_look.x = Input.GetAxis("Mouse X") * speed_look.x;
            input_look.y = Input.GetAxis("Mouse Y") * speed_look.y;
        }
    }
}