using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Sources ----------")]
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clips -----------")]
    public AudioClip highlightCardSFX;
    public AudioClip ambient;
    public AudioClip getsCard;
    public AudioClip putCard;

    public void PlaySFX(AudioClip sFX)
    {
        SFXSource.PlayOneShot(sFX);
    }

}
