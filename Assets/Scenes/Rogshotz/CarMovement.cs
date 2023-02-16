using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed = 0f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform carTrigger;
    [SerializeField] private LayerMask inCamera;
    // Update is called once per frame
    void Update()
    {
        if (InCamera())
        {
            // Debug.Log("Car Trigger");
            speed = -4f;
        } else {
            speed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);
    }
    private bool InCamera()
    {
        return Physics2D.OverlapCircle(carTrigger.position, 0.2f, inCamera);
    }
}
