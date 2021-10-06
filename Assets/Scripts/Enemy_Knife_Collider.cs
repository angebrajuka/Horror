using UnityEngine;

public class Enemy_Knife_Collider : MonoBehaviour
{
    // hierarchy
    public Enemy_Knife knife;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == Layers.PLAYER && knife.towards)
        {
            knife.towards = false;
            knife.speed = 1;
            PlayerBloodUI.AddSplatter();
            PlayerLife.Health -= 4;
        }
    }
}
