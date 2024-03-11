using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 cameraPosition = new Vector3(0, 0, -10);

    void FixedUpdate()
    {
        transform.position = player.transform.position + cameraPosition;
    }
}
