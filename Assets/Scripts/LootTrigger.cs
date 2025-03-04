using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTrigger : MonoBehaviour
{
    [SerializeField] private int minXP = 1;
    [SerializeField] private int maxXP = 5;
    [SerializeField] private int minGold = 1;
    [SerializeField] private int maxGold = 5;
    private ParticleSystem lootFX;
    private MeshRenderer lootRenderer;
    private AudioSource pickupSFX;

    private void OnEnable()
    {
        lootFX = GetComponent<ParticleSystem>();
        lootRenderer = GetComponent<MeshRenderer>();
        pickupSFX = GetComponent<AudioSource>();
        lootRenderer.enabled = true;
        transform.Translate(Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player playerTriggering = other.GetComponent<Player>();
        if (playerTriggering != null)
        {
            playerTriggering.GainLoot(Random.Range(minGold, maxGold), Random.Range(minXP, maxXP));
            pickupSFX.Play();
            StartCoroutine(DeactivateAfterFX());
        }
    }

    private IEnumerator DeactivateAfterFX()
    {
        lootFX.Play();
        lootRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
