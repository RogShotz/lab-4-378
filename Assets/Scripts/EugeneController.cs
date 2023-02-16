using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EugeneController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public float distanceFromCameraCenter = -17;

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
