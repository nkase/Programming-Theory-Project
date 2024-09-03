using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private bool switchState = false;
    public override void Interact(GameObject interactor)
    {
        if (switchState == false)
        {
            transform.Translate(new Vector3(0, 1, 0));
            switchState = true;
        }
        else
        {
            transform.Translate(new Vector3(0, -1, 0));
            switchState = false;
        }
    }
}
