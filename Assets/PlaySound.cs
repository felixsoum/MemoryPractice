using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = audioClip;
    }
    public void PlayBtnSound()
    {
        audioSource.Play();
    }
}
