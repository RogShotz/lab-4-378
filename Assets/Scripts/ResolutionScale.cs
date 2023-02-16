using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScale : MonoBehaviour
{
    [SerializeField] private GameObject cameraCheck;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = cameraCheck.GetComponent<Renderer>().bounds.size;
        Vector2 rescale = cameraCheck.transform.localScale;
        Vector2 real_size = new Vector2(Screen.width / 27 * rescale.x / size.x, Screen.height / 27 * rescale.y / size.y);
        cameraCheck.transform.localScale = real_size;
    }
}
