using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject parent;
    private Actor parentActorScript;
    private float speed = 10;

    private void OnEnable()
    {
        parent = transform.parent.gameObject;
        parentActorScript = parent.GetComponent<Actor>();
        StartCoroutine(DeactivateAfterLifetime());
        Debug.Log("Projectile facing:" + transform.forward);
        transform.Translate(new Vector3(0, 1, 0));
    }

    void Update()
    {
        //transform.position += (transform.forward * Time.deltaTime);
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor otherActor = other.GetComponent<Actor>();
        if (otherActor != null)
        {
            if (other.gameObject != transform.parent.gameObject)
            {
                otherActor.Damage(parentActorScript.attackDamage);
            }
        }
    }

    private IEnumerator DeactivateAfterLifetime()
    {
        yield return new WaitForSeconds(parentActorScript.attackRange);
        gameObject.SetActive(false);
    }
}
