using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float yRotationOnOpen = 90;
    private bool isOpen;
    private float rotationSpeed = 100;
    private float rotationTimer;

    private void Update()
    {
        //if (isOpen && transform.rotation != endRotation)
        //{
        //    transform.Rotate(0, yRotationOnOpen *  Time.deltaTime, 0);
        //}

        //if (!isOpen && transform.rotation != startRotation)
        //{
        //    transform.Rotate(0, -yRotationOnOpen * Time.deltaTime, 0);
        //}
        if (rotationTimer < 0)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            rotationTimer += (rotationSpeed * Time.deltaTime);
        }
        if (rotationTimer > 0)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            rotationTimer -= (rotationSpeed * Time.deltaTime);
        }
    }


    public void Open()
    {
        rotationTimer += yRotationOnOpen;
    }

    public void Close()
    {
        rotationTimer -= yRotationOnOpen;
    }
}
