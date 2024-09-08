using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlighter : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private void OnTriggerEnter(Collider other)
    {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (otherInteractable != null)
        {
            //otherInteractable.Glow();
            highlight.transform.position = otherInteractable.transform.position + (transform.up * 5);
            highlight.transform.SetParent(otherInteractable.transform, true);
            highlight.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (otherInteractable != null)
        {
            //otherInteractable.StopGlow();
            highlight.transform.position = transform.position;
            highlight.transform.SetParent(null);
            highlight.SetActive(false);
        }
    }
}
