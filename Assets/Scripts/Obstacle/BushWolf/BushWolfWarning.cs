using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushWolfWarning : MonoBehaviour
{
    [SerializeField] BushWolf bushWolf;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            bushWolf.Warning();
    }
}
