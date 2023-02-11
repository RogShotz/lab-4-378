using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public float speed = 10f;
    public Transform player;
 
    // Update is called once per frame
    void Update () {
        transform.position =  new Vector3(transform.position.x + (1 * Time.deltaTime * speed), player.transform.position.y + 2, -5);

    }

}