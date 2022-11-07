using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("temp")]
    [SerializeField] Transform model;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float addSpeed;
    private Rigidbody2D playerRigidbody;
    private CapsuleCollider2D playerCollider;
    private float runFasterTime = 0.5f;
    private float runFasterTimer;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    private bool jumpInput;
    private bool jumpInputReleased;
    private bool isJumping;

    [Header("Slide")]
    [SerializeField] float slideTime;
    [SerializeField] Quaternion slidingTargetRotation;
    [SerializeField] Vector2 colliderTargetSize;
    private bool slideInput;
    private bool slideInputRelease;
    private bool isSliding;
    private float slidingTimer;
    private Quaternion originalRotation;
    private Vector2 colliderOriginalSize;

    [Header("JumpCut")]
    [SerializeField] bool jumpCut;
    [SerializeField] float jumpCutMultiplier;

    [Header("Coyote Time")]
    [SerializeField] bool coyoteTime;
    [SerializeField] float jumpBufferTime;
    [SerializeField] float groundedTime;
    [SerializeField] float coyoteJumpForce;
    private float lastGroundedTime;
    private float lastJumpTime;

    [Header("Movement READONLY")]
    [SerializeField] private float moveForward;


    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody
        playerRigidbody = GetComponent<Rigidbody2D>();

        // Collider and get collider original size
        playerCollider = GetComponent<CapsuleCollider2D>();
        colliderOriginalSize = playerCollider.size;

        // Get original rotation
        originalRotation = transform.rotation;

        // Setting move forward, run faster timer, sliding timer
        moveForward = moveSpeed;
        runFasterTimer = runFasterTime;
        slidingTimer = slideTime;

        //Debug.Log(moveForward);
    }

    private void Update()
    {
        // Check if the circle overlaps with the ground.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputReleased = Input.GetButtonUp("Jump");
        slideInput = Input.GetButtonDown("Slide");
        slideInputRelease = Input.GetButtonUp("Slide");
        
        if(isGrounded)
        {
            isJumping = false;
        }

        // Jump with or without coyote time
        if (coyoteTime)
        {
            if (isGrounded)
            {
                lastGroundedTime = groundedTime;
            }
            if (isGrounded && jumpInput)
            {
                Jump();
            } else if(lastGroundedTime > 0 && lastJumpTime < 0 && !isJumping && jumpInput)
            {
                CoyoteJump();
            }
        } else {
            if (jumpInput && isGrounded)
            {
                Jump();
            }
        }

        // Jump Cut
        if (jumpInputReleased && jumpCut)
        {
            JumpCut();
        }

        // Sliding
        if(slideInput)
        {
            isSliding = true;
            SlideCheck();
        }

        if(slideInputRelease || slidingTimer <= 0)
        {
            isSliding = false;
            SlideCheck();
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

        if(isSliding)
        {
            slidingTimer -= Time.deltaTime;
        }

        Move();
    }

    void Jump()
    {
        if(coyoteTime)
        {
            lastJumpTime = jumpBufferTime;
        }
        isJumping = true;
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //Debug.Log("Jump!");
    }

    void CoyoteJump()
    {
        lastJumpTime = jumpBufferTime;
        isJumping = true;
        playerRigidbody.AddForce(Vector2.up * coyoteJumpForce, ForceMode2D.Impulse);
    }

    void JumpCut()
    {
        if (playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.AddForce(Vector2.down * playerRigidbody.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            Debug.Log("jump cut");
        }
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

    void SlideCheck()
    {
        slidingTimer = slideTime;

        if (isSliding)
        {
            // Rotation just for animation
            model.rotation = Quaternion.Lerp(transform.rotation, slidingTargetRotation, 1f);

            // Change Collider Size
            playerCollider.direction = CapsuleDirection2D.Horizontal;
            playerCollider.size = colliderTargetSize;

            Debug.Log("Sliding");
        } else if (!isSliding)
        {
            // Rotation just for animation
            model.rotation = Quaternion.Lerp(transform.rotation, originalRotation, 1f);

            // Change Collider Size
            playerCollider.direction = CapsuleDirection2D.Vertical;
            playerCollider.size = colliderOriginalSize;
            Debug.Log("Stop sliding");
        }
    }
}
