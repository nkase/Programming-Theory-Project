using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Block : Interactable
{
    private float speed = 0.8f;

    public override void Interact(GameObject interactor)
    {
        Debug.Log("Maybe I can push this?");
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.otherCollider.GetComponentInParent<Player>() != null)
            {
                //Debug.Log(contact.normal.x + " " + contact.normal.y + " " + contact.normal.z);
                transform.Translate(Time.deltaTime * speed * contact.normal);
                //transform.position = Vector3.Lerp(transform.position, Time.deltaTime * speed * contact.normal + transform.position, 1);
                //contact.otherCollider.attachedRigidbody.AddForce(-contact.normal * , ForceMode.Impulse);
            }
        }
    }
}
