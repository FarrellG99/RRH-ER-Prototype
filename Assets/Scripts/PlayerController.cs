using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float addSpeed;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody2D playerRigidbody;
    private float runFasterTime = 0.5f;
    private float runFasterTimer;
    private bool isGrounded;
    private bool jumpInput;
    private bool isJumping;

    [Header("Coyote Time")]
    [SerializeField] bool coyoteTime;
    [SerializeField] float jumpBufferTime;
    [SerializeField] float groundedTime;
    private float lastGroundedTime;
    private float lastJumpTime;

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
        // Check if the circle overlaps with the ground.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
        jumpInput = Input.GetButtonDown("Jump");
        if (coyoteTime)
        {
            if (isGrounded)
            {
                isJumping = false;
                lastGroundedTime = groundedTime;
            }
            if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping && jumpInput)
            {
                Jump();
            }
        } else {
            if (jumpInput && isGrounded)
            {
                Jump();
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Coyote Timer
        if(coyoteTime)
        {
            lastGroundedTime -= Time.deltaTime;
            lastJumpTime -= Time.deltaTime;
        }
        #endregion

        Move();
    }

    void Jump()
    {
        if(coyoteTime)
        {
            isJumping = true;
        }
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump!");
    }

    void Move()
    {
        runFasterTimer -= Time.deltaTime;
        if (runFasterTimer <= 0)
        {
            runFasterTimer = runFasterTime;
            moveForward += addSpeed;
            //Debug.Log(moveForward);
        }

        playerRigidbody.velocity = new Vector2(moveForward * Time.deltaTime, playerRigidbody.velocity.y);
    }
}
