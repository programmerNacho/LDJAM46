﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private AudioSource currentAudioPlaceSelected;

    public void SelectAudioPlace(AudioSource audioPlace)
    {
        currentAudioPlaceSelected = audioPlace;
    }

    public void PlayDialogue(ActorDialogue audioClip)
    {
        if(currentAudioPlaceSelected)
        {
            currentAudioPlaceSelected.Stop();
            currentAudioPlaceSelected.clip = audioClip.dialogues[Random.Range(0, audioClip.dialogues.Length)];
            currentAudioPlaceSelected.Play();
        }
    }
}
