using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ABSTRACTION
// Honestly, Interactable is acting more as a tag than a class, since every interactable has a different interaction.
public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(GameObject interactor);
}
