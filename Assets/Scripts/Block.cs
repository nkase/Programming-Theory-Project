using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Interactable
{
    private Vector3 offset;
    private GameObject pusher;
    private bool isBeingPushed = false;

    public override void Interact(GameObject interactor)
    {
        if (isBeingPushed == false)
        {
            pusher = interactor;
            offset = interactor.transform.position - transform.position;
            isBeingPushed = true;
        }
        else
        {
            pusher = null;
            isBeingPushed = false;
        }
    }

    private void Update()
    {
        if (isBeingPushed == true)
        {
            transform.position = pusher.transform.position - offset;
        }
    }
}
