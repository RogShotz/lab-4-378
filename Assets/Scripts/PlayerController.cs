using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float maxSpeed = 7f;
    public float acceleration = 500f;
    public float jumpForce = 6f;
    public float jumpHold = 0.35f;
    public float crouchTransTime = 0.15f;
    private float jumpTimer = 0f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool hasLiftedOff = false; // Used in Alex_Jump animation event
    private Rigidbody2D rb;

    private BoxCollider2D bc;
    private Vector2 defaultColliderOffset; 
    private Vector2 defaultColliderSize; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();
        defaultColliderOffset = bc.offset;
        defaultColliderSize = bc.size;
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < maxSpeed)
            rb.AddForce(new Vector2(acceleration * Time.fixedDeltaTime, 0) * rb.mass);

        if (IsGrounded() && animator.speed == 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Alex_Jump")
        {
            hasLiftedOff = false;
            animator.speed = 1;
            animator.SetBool("IsJumping", false);
        }
        
        // adapted from https://www.youtube.com/watch?v=j111eKN8sJw
        if (isJumping) {
            if (IsGrounded()) jumpTimer = jumpHold;
            if (jumpTimer > 0 && hasLiftedOff) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimer -= Time.fixedDeltaTime;
            }
        }

        if (isCrouching) {
            Vector2 targetColliderOffset = new Vector2(bc.offset.x, -(3f/32f));
            Vector2 targetColliderSize = new Vector2(bc.size.x, defaultColliderSize.y - (6f/32f));
            bc.offset = Vector2.Lerp(bc.offset, targetColliderOffset, Time.fixedDeltaTime / crouchTransTime);
            bc.size = Vector2.Lerp(bc.size, targetColliderSize, Time.fixedDeltaTime / crouchTransTime);
            animator.SetBool("IsCrouching", true);
        } else {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            animator.speed = 1;
            bc.offset = Vector2.Lerp(bc.offset, defaultColliderOffset, Time.fixedDeltaTime / crouchTransTime);
            bc.size = Vector2.Lerp(bc.size, defaultColliderSize, Time.fixedDeltaTime / crouchTransTime);
        }

        maxSpeed += Time.fixedDeltaTime * 0.1f; // Increase max speed by 1 every 10 seconds
    }

    void HoldPose()
    {
        animator.speed = 0;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed) {
            if (IsGrounded()) jumpTimer = jumpHold;
            else jumpTimer = 0;
            isJumping = true;
            animator.SetBool("IsJumping", true);
        } else {
            isJumping = false;
        }
    }

    void OnCrouch(InputValue value)
    {
        if (value.isPressed) isCrouching = true;
        else isCrouching = false;
    }

    void LiftedOff()
    {
        hasLiftedOff = true;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(
            transform.position - new Vector3(0, transform.localScale.y / 2f, 0),
            new Vector2(transform.localScale.x / 2f - 0.1f, 0.1f),
            0,
            LayerMask.GetMask("Ground")
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position - new Vector3(0, transform.localScale.y / 2f, 0),
            new Vector2(transform.localScale.x / 2f - 0.1f, 0.1f)
        );
    }
}
