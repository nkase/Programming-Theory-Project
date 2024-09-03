using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEventListener : MonoBehaviour
{
    [SerializeField] private int hostility = 0;
    private AudioSource[] audioSources;

    private void OnEnable()
    {
        Monster.onMonsterHostility += increaseHostility;
        Monster.onMonsterPassivity += decreaseHostility;
        audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (hostility >= 1)
        {
            audioSources[3].volume = Mathf.Clamp(audioSources[3].volume + (Time.deltaTime * hostility / 2 ), 0, 1);
        }
        if (hostility < 1)
        {
            audioSources[3].volume = Mathf.Clamp(audioSources[3].volume - (Time.deltaTime / 2), 0, 1);
        }
    }

    private void decreaseHostility()
    {
        hostility--;
    }

    private void increaseHostility()
    {
        hostility++;
    }
}
