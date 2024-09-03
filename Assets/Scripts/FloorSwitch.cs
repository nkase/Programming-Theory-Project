using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [SerializeField]
    private Door triggerTarget;
    public bool trigger;
    public int currentCollisions;

    private void OnCollisionEnter()
    {
        currentCollisions++;

        if (trigger != true)
        {
            trigger = true;
            triggerTarget.Open();
        }
    }

    private void OnCollisionExit()
    {
        currentCollisions--;

        if (currentCollisions == 0 )
        {
            trigger = false;
            triggerTarget.Close();
        }
    }
}
