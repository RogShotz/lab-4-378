using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EugeneController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject mainCamera;
    public float distanceFromCameraCenter; // TODO do this programmatically
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        animator.speed = 0.25f;
        emission = transform.Find("Particles").GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        
    }

    void Update()
    {
        if (transform.position.x <= player.transform.position.x)
        {
            Vector2 resultingPosition = new Vector2(
                mainCamera.transform.position.x + distanceFromCameraCenter, 
                transform.position.y
            );
            transform.position = resultingPosition;
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0, LayerMask.GetMask("Ground")))
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
