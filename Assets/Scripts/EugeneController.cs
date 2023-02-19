using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EugeneController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject mainCamera;
    public float distanceFromCameraCenter; // TODO do this programmatically

    void Start()
    {
        animator.speed = 0.25f;
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
}
