using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EugeneController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject mainCamera;
    public Vector3 startingPosition; // TODO do this programmatically
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        animator.speed = 0.25f;
        emission = transform.Find("Particles").GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        startingPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = startingPosition;
    }

    void Update()
    {
        float speed = player.GetComponent<PlayerController>().maxSpeed;

        transform.position = new Vector2(
            transform.position.x + speed * Time.deltaTime,
            transform.position.y
        );
    }

    void FixedUpdate()
    {
        string[] layers = {"Ground", "Fatal"};
        if (Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0, LayerMask.GetMask(layers)))
        {
            animator.speed = 1f;
            emission.enabled = true;
        } else {
            animator.speed = 0.25f;
            emission.enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 0.5f, 0));
    }
}
