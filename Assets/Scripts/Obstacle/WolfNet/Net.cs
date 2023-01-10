using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameplayManager.Gameplay.IsPlaying = false;
            Debug.Log("Get Caught Wolf Net");
        }
    }
}
