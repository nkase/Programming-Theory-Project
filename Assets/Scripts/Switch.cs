using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private bool switchState = false;
    [SerializeField] private GameObject lever;
    [SerializeField] private Door triggerTarget;
    public override void Interact(GameObject interactor)
    {
        if (switchState == false)
        {
            lever.transform.Rotate(-60, 0, 0);
            triggerTarget.Open();
            switchState = true;
        }
        else
        {
            lever.transform.Rotate(60, 0, 0);
            triggerTarget.Close();
            switchState = false;
        }
    }
}
