using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Chest : Interactable
{
    [SerializeField] private GameObject lid;

    // POLYMORPHISM
    public override void Interact(GameObject interactor)
    {
        GameObject loot = ObjectPool.SharedInstance.GetPooledObject();
        if (loot != null)
        {
            loot.transform.position = transform.position + transform.forward;
            loot.transform.rotation = transform.rotation;
            loot.SetActive(true);
        }

        // POLYMORPHISM?
        // Different behaviours based on what's available on the object. Could be separated into two classes.
        if (lid == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            lid.transform.Rotate(-45, 0, 0);
        }
    }
}
