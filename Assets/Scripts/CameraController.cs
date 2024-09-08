using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ABSTRACTION
// Widely applicable behaviour - rename to TransformToTarget, rename "player" to "target" and use it wherever needed.
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    // mouseX = Input.GetAxis("Mouse X");
        //    //transform.Rotate(0, mouseX * lookSpeed * Time.deltaTime, 0);
        //    transform.Rotate(0, 45, 0);
        //}

        transform.position = player.transform.position;
    }
}
