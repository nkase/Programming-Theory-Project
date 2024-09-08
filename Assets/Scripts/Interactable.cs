using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ABSTRACTION
// Honestly, Interactable is acting more as a tag than a class, since every interactable has a different interaction.
public abstract class Interactable : MonoBehaviour
{
    //private Material material;

    //private void Start()
    //{
    //    material = GetComponent<Material>();
    //}

    public abstract void Interact(GameObject interactor);

    ////public void Glow()
    //{
    //    material.SetColor("_EmissionColor", Color.cyan * 1);
    //}

    //public void StopGlow()
    //{
    //    material.SetColor("_EmissionColor", Color.cyan * 0);
    //}
}
