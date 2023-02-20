using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public float CamLerpSpeed = 2f;
    public Vector3 TargetPosition;

    void Start()
    {
        TargetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            TargetPosition,
            CamLerpSpeed * Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        TargetPosition = new Vector3(
            TargetPosition.x + player.maxSpeed * Time.fixedDeltaTime,
            player.transform.position.y,
            transform.position.z
        );
    }

    Vector3 RoundPosition(Vector3 v3)
    {
        return new Vector3(
            (float)decimal.Round((decimal)v3.x),
            (float)decimal.Round((decimal)v3.y),
            (float)decimal.Round((decimal)v3.z)
        );
    }
}
