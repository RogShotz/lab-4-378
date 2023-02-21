using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private GameObject enemy;
    public float movementAmountPerUpdate = 2f;
    public float triggerDistance = 10f;
    private bool isMoving = false;
    private Vector3 startingPosition;

    void Start()
    {
        enemy = GameObject.Find("Eugene");
        startingPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = startingPosition;
        isMoving = false;
    }

    void Update()
    {
        if (enemy.transform.position.x > transform.position.x + triggerDistance + transform.localScale.x/2)
        {
            gameObject.SetActive(false);
        } 
        else if (isMoving)
        {
            transform.position = new Vector2(
                transform.position.x - movementAmountPerUpdate * Time.deltaTime,
                transform.position.y
            );
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapBox(
            transform.position - new Vector3(
                triggerDistance + transform.localScale.x/2f, 0, 0
            ),
            new Vector2(1f, 20f),
            0,
            LayerMask.GetMask("Player")
        )) {
            isMoving = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            transform.position - new Vector3(
                triggerDistance + transform.localScale.x/2f, 0, 0
            ),
            new Vector3(1f, 20f, 0)
        );
    }
}
