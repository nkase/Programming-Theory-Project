using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Quaternion startRotation;
    private Quaternion endRotation;
    [SerializeField]
    private float yRotationOnOpen = 90;
    private bool isOpen;

    private void Start()
    {
        startRotation = transform.rotation;
        transform.Rotate(0, yRotationOnOpen, 0);
        endRotation = transform.rotation;
        transform.Rotate(0, -yRotationOnOpen, 0);
    }

    private void Update()
    {
        if (isOpen && transform.rotation != endRotation)
        {
            transform.Rotate(0, yRotationOnOpen *  Time.deltaTime, 0);
        }

        if (!isOpen && transform.rotation != startRotation)
        {
            transform.Rotate(0, -yRotationOnOpen * Time.deltaTime, 0);
        }
    }


    public void Open()
    {
        isOpen = true;
    }

    public void Close()
    {
        isOpen = false;
    }
}
