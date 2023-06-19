using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushWolf : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Animator animator;

    public void Warning()
    {
        animator.SetBool("Warning", true);
        Debug.Log("Warning");
    }

    public void Surprised(PlayerController player)
    {
        animator.SetTrigger("Surprised");
        Debug.Log("Surprised");
        player.Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Surprised(collision.GetComponent<PlayerController>());
        }
    }
}
