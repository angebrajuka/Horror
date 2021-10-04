using UnityEngine;

public static class PlayerLife
{
    private static int health;
    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health < 0)
            {
                dead = true;
                Debug.Log("dead");
                System.IO.File.Delete(SaveData.FILE_PATH);
                Application.Quit();
            }
        }
    }
    public static bool dead;

    public static void Reset()
    {
        health = 100;
        dead = false;
    }
}