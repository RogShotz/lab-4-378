using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private GameObject enemy;
    public float movementAmountPerUpdate = 0.01f;
    public Animator animator;
    enum Type { Blue, Red, Silver};
    [SerializeField] Type type = new Type();

    void Start()
    {
        enemy = GameObject.Find("Eugene");
        animator.speed = 0.5f;
        switch (type)
        {
            case Type.Blue:
                animator.SetInteger("Color", 0);
                break;
            case Type.Red:
                animator.SetInteger("Color", 1);
                break;
            case Type.Silver:
                animator.SetInteger("Color", 2);
                break;
            default:
                Debug.LogError("Car color not recognized");
                break;
        }
    }

    void Update()
    {
        if (enemy.transform.position.x > transform.position.x + 2f)
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
