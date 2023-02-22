using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonController : MonoBehaviour
{
    public float frequency = 5f;
    public float amplitude = 0.5f;

    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, initialY + offset, transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        // travel area indicator
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(transform.position.x, transform.position.y + amplitude, transform.position.z),
            new Vector3(transform.position.x, transform.position.y - amplitude, transform.position.z)
        );
    }
}
