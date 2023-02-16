using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float maxSpeed = 5f;
    public float acceleration = 500f;
    public float jumpForce = 6f;
    public float jumpHold = 0.35f;
    public float crouchTransTime = 0.15f;
    private float maxSpeedTarget;
    private float jumpTimer = 0f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private Rigidbody2D rb;

    private BoxCollider2D bc;
    private Vector2 defaultColliderOffset;
    private Vector2 defaultColliderSize;
    [SerializeField] private LayerMask inCamera;
    private float move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();
        defaultColliderOffset = bc.offset;
        defaultColliderSize = bc.size;
        maxSpeedTarget = maxSpeed;
    }

    void FixedUpdate()
    {
        if (!InCamera())
        {
            Debug.Log("Dead");
            Time.timeScale = 0f;
        }

        if (rb.velocity.x < maxSpeed + move)
            rb.AddForce(new Vector2(acceleration * Time.fixedDeltaTime, 0) * rb.mass);
        maxSpeedTarget = Mathf.Clamp(rb.velocity.x, 0, maxSpeed + move);
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(maxSpeedTarget, rb.velocity.y), Time.fixedDeltaTime * 6f);

        if (IsGrounded() && animator.speed == 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Alex_Jump")
        {
            animator.speed = 1;
            animator.SetBool("IsJumping", false);
        }

        // adapted from https://www.youtube.com/watch?v=j111eKN8sJw
        if (isJumping)
        {
            if (IsGrounded()) jumpTimer = jumpHold;
            if (jumpTimer > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimer -= Time.fixedDeltaTime;
            }
        }

        if (isCrouching)
        {
            Vector2 targetColliderOffset = new Vector2(bc.offset.x, -(3f / 32f));
            Vector2 targetColliderSize = new Vector2(bc.size.x, defaultColliderSize.y - (6f / 32f));
            bc.offset = Vector2.Lerp(bc.offset, targetColliderOffset, Time.fixedDeltaTime / crouchTransTime);
            bc.size = Vector2.Lerp(bc.size, targetColliderSize, Time.fixedDeltaTime / crouchTransTime);
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
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
        if (value.isPressed)
        {
            if (IsGrounded()) jumpTimer = jumpHold;
            else jumpTimer = 0;
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
        else
        {
            isJumping = false;
        }
    }

    void OnCrouch(InputValue value)
    {
        if (value.isPressed) isCrouching = true;
        else
        {
            isCrouching = false;
            animator.speed = 1;
        }
    }

    void OnMove(InputValue value)
    {
        if (value.Get<Vector2>() != Vector2.zero) 
        {
            move = value.Get<Vector2>().x * 4f;
            if (move > 0) animator.SetBool("IsSpeedingUp", true);
        }
        else animator.SetBool("IsSpeedingUp", false);
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

    private bool InCamera()
    {
        return Physics2D.OverlapCircle(rb.position, 0.2f, inCamera);
    }
}
