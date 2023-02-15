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
    private float jumpTimer = 0f;
    private bool isJumping = false;
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
        if (value.isPressed) {
            bc.offset = new Vector2(bc.offset.x, bc.offset.y * -100);
            bc.size = new Vector2(bc.size.x, bc.size.y * 0.8125f);
            animator.SetBool("IsCrouching", true);
        } else {
            animator.SetBool("IsCrouching", false);
            animator.speed = 1;
            bc.offset = defaultColliderOffset;
            bc.size = defaultColliderSize;
        }
    }

    void LiftedOff()
    {
        hasLiftedOff = true;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(
            transform.position - new Vector3(0, transform.localScale.y / 6, 0),
            new Vector2(transform.localScale.x - 0.1f, 0.1f),
            0,
            LayerMask.GetMask("Ground")
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position - new Vector3(0, transform.localScale.y / 6, 0),
            new Vector2(transform.localScale.x - 0.1f, 0.1f)
        );
    }
}
