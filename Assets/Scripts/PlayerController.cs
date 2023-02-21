using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public AudioSource ridingAudioSource;
    public AudioSource landingAudioSource;
    public AudioSource collisionAudioSource;

    public float maxSpeed = 5f;
    public float acceleration = 500f;
    public float jumpForce = 6f;
    public float jumpHold = 0.35f;
    public float crouchTransTime = 0.15f;
    public GameObject deathPanel;
    private Vector3 startingPosition;
    private float maxSpeedStart;
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
        ridingAudioSource = GameObject.Find("Skateboard Riding").GetComponent<AudioSource>();
        landingAudioSource = GameObject.Find("Skateboard Landing").GetComponent<AudioSource>();
        collisionAudioSource = GameObject.Find("Skateboard Stopping").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        defaultColliderOffset = bc.offset;
        defaultColliderSize = bc.size;
        maxSpeedTarget = maxSpeed;
        maxSpeedStart = maxSpeed;

        startingPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = startingPosition;
        rb.velocity = Vector2.zero;
        maxSpeed = maxSpeedStart;
        maxSpeedTarget = maxSpeed;
        isJumping = false;
        isCrouching = false;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsCrouching", false);
        animator.SetBool("IsSpeedingUp", false);
        animator.speed = 1f;

        bc.offset = defaultColliderOffset;
        bc.size = defaultColliderSize;

        CarController[] cars = GameObject.FindObjectsOfType<CarController>();
        foreach (CarController car in cars) car.Reset();
        CameraController camera = GameObject.FindObjectOfType<CameraController>();
        camera.Reset();
        EugeneController eugene = GameObject.FindObjectOfType<EugeneController>();
        eugene.Reset();

        deathPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        InCameraUpdates(); // if (!InCamera()) {...}

        if (rb.velocity.x < maxSpeed + move)
            rb.AddForce(new Vector2(acceleration * Time.fixedDeltaTime, 0) * rb.mass);
        maxSpeedTarget = Mathf.Clamp(rb.velocity.x, 0, maxSpeed + move);
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(maxSpeedTarget, rb.velocity.y), Time.fixedDeltaTime * 6f);

        IsGroundedUpdates(); // if (IsGrounded()) {...}
        IsJumpingUpdates(); // if (isJumping) {...}
        IsCrouchingUpdates(); // if (isCrouching) {...}

        maxSpeed += Time.fixedDeltaTime * 0.1f; // Increase max speed by 1 every 10 seconds
    }

    private void InCameraUpdates()
    {
        if (!InCamera()) {
            OnDeath();
        }
    }

    private void IsGroundedUpdates()
    {
        if (IsGrounded())
        {
            if (animator.speed == 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Alex_Jump")
            {
                animator.speed = 1;
                animator.SetBool("IsJumping", false);
            }

            if (!ridingAudioSource.isPlaying)
            {
                ridingAudioSource.Play();
                landingAudioSource.Play();
            }
        }
        else
        {
            ridingAudioSource.Pause();
        }
    }

    private void IsJumpingUpdates()
    {
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
    }

    private void IsCrouchingUpdates() 
    {
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
        move = value.Get<Vector2>().x * 4f;
        if (move > 0) animator.SetBool("IsSpeedingUp", true);
        else animator.SetBool("IsSpeedingUp", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            collisionAudioSource.Play();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fatal"))
        {
            OnDeath();
        }
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

    public void OnDeath()
    {
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
    }
}
