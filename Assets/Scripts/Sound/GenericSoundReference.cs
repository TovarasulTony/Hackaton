using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSoundReference : MonoBehaviour
{
    protected Dictionary<string, List<AudioClip>> m_AudioClipDictionary;

    public GenericSoundReference()
    {
        m_AudioClipDictionary = new Dictionary<string, List<AudioClip>>();
    }

    public void Play(string _subcategory)
    {
        AudioSource audio = GetComponent<AudioSource>();
        List<AudioClip> audioList = m_AudioClipDictionary[_subcategory];
        audio.clip = audioList[Random.Range(0, audioList.Count)];
        audio.Play(0);
    }
}
