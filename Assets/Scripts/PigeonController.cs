using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonController : MonoBehaviour
{

    public float moveSpace;
    void Update()
    {
        transform.position = new Vector2(transform.position.x - moveSpace, transform.position.y);
    }
}
