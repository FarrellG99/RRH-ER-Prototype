using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushWolf : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Warning()
    {
        animator.SetBool("Warning", true);
        Debug.Log("Warning");
    }

    public void Surprised()
    {
        animator.SetTrigger("Surprised");
        Debug.Log("Surprised");
        GameplayManager.Gameplay.IsPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Surprised();
        }
    }
}
