using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float addSpeed;
    private Rigidbody2D playerRigidbody;
    private float runFasterTime = 0.5f;
    private float runFasterTimer;

    [Header("Movement READONLY")]
    [SerializeField] private float moveForward;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        moveForward = moveSpeed;
        runFasterTimer = runFasterTime;

        //Debug.Log(moveForward);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump")) {
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        runFasterTimer -= Time.deltaTime;
        if(runFasterTimer <= 0)
        {
            runFasterTimer = runFasterTime;
            moveForward += addSpeed;
            //Debug.Log(moveForward);
        }

        playerRigidbody.velocity = new Vector2(moveForward * Time.deltaTime, playerRigidbody.velocity.y);
    }
}
