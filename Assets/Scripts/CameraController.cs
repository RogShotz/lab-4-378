using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float CamLerpSpeed = 2f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(player.position.x, transform.position.y, transform.position.z),
            CamLerpSpeed * Time.deltaTime
        );
    }
}
