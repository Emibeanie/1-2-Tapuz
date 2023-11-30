using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaultPlaySound : MonoBehaviour
{
    [SerializeField] AudioSource vaultAudioSource;
    private void soundPlay()
    {
        vaultAudioSource.Play();
    }
}
