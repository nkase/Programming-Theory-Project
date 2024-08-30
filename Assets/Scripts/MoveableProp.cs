using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableProp : Interactable
{
    private Vector3 offset;
    private GameObject carrier;
    private bool isBeingCarried = false;

    public override void Interact(GameObject interactor)
    {
        if (isBeingCarried == false)
        {
            carrier = interactor;
            offset = interactor.transform.position - transform.position;
            isBeingCarried = true;
        }
        else
        {
            carrier = null;
            isBeingCarried = false;
        }
    }

    private void Update()
    {
        if (isBeingCarried == true)
        {
            transform.position = carrier.transform.position - offset;
        }
    }
}
