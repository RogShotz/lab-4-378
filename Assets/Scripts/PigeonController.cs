using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonController : MonoBehaviour
{
    private GameObject enemy;
    public float movementAmountPerUpdate = 2f;

    void Start()
    {
        enemy = GameObject.Find("Eugene");
    }

    void Update()
    {
        if (enemy.transform.position.x > transform.position.x)
        {
            transform.position = new Vector2(
                transform.position.x - movementAmountPerUpdate, 
                transform.position.y + movementAmountPerUpdate
            );
        } 
        else if (enemy.transform.position.x > transform.position.x + 2f)
        {
            Destroy(gameObject);
        } 
        else
        {
            transform.position = new Vector2(
                transform.position.x - movementAmountPerUpdate, 
                transform.position.y
            );
        }
    }
}
