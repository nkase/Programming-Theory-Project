using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    [SerializeField] private bool switchState = false;
    [SerializeField] private GameObject lever;
    [SerializeField] private Door triggerTarget;
    private AudioSource soundEffect;

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    public override void Interact(GameObject interactor)
    {
        if (switchState == false)
        {
            lever.transform.Rotate(-60, 0, 0);
            triggerTarget.Open();
            soundEffect.Play();
            switchState = true;
        }
        else if (switchState == true)
        {
            lever.transform.Rotate(60, 0, 0);
            triggerTarget.Close();
            soundEffect.Play();
            switchState = false;
        }
    }
}
