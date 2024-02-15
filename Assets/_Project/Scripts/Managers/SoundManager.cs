using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
