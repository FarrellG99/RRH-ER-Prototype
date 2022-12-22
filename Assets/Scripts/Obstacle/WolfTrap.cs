using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FallIntoTheTrap();
        }
    }

    private void FallIntoTheTrap()
    {
        GameplayManager.Gameplay.IsPlaying = false;
        GameplayManager.Gameplay.IsFallIntoTheTrap = true;
        Debug.Log("Fall into the trap");
    }
}
