using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FallIntoTheTrap(collision.GetComponent<PlayerController>());
        }
    }

    private void FallIntoTheTrap(PlayerController player)
    {
        player.Death();
        GameplayManager.Instance.IsFallIntoTheTrap = true;
        Debug.Log("Fall into the trap");
    }
}
