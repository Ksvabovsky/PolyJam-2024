using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterTemplate", menuName = "Characters/Templates/CharacterSO", order = 1)]
public class CharacterTemplate : ScriptableObject
{
    public string name;
    public List<AudioClip> voiceClips = new List<AudioClip>();

    public AudioClip GetVoiceClip()
    {
        int clipNumber = Random.Range(0, voiceClips.Count - 1);
        return voiceClips[clipNumber];
    }
}
