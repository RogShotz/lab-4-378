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
    private bool hasLiftedOff = false; // Used with jumping animation
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < maxSpeed)
            rb.AddForce(new Vector2(acceleration * Time.fixedDeltaTime, 0) * rb.mass);
        
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

    void OnJump(InputValue value)
    {
        if (value.isPressed) {
            if (IsGrounded()) jumpTimer = jumpHold;
            else jumpTimer = 0;
            isJumping = true;
        } else {
            isJumping = false;
        }
        animator.SetBool("IsJumping", isJumping);
    }

    void OnCrouch(InputValue value)
    {
        // TODO: Implement crouching
        if (value.isPressed) {
            // Crouch
        } else {
            // Stand up
        }
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

    void LiftedOff()
    {
        hasLiftedOff = true;
    }

    void Landed()
    {
        hasLiftedOff = false;
    }

    void OnDrawGizmos()
    {
        // float height = (RectTransform).rect.height;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position - new Vector3(0, transform.localScale.y / 6, 0),
            new Vector2(transform.localScale.x - 0.1f, 0.1f)
        );
    }
}