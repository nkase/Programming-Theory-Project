using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float lookSpeed = 500;
    private float mouseX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(0, mouseX * lookSpeed * Time.deltaTime, 0);
        }

        transform.position = Vector3.Lerp(transform.position, player.transform.position,5);
    }
}
