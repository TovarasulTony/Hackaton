using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : GenericSoundReference
{
    [SerializeField]
    List<AudioClip> m_MeleSounds;
    [SerializeField]
    List<AudioClip> m_DigSounds;

    void Awake()
    {
        m_AudioClipDictionary.Add("mele", m_MeleSounds);
        m_AudioClipDictionary.Add("dig", m_DigSounds);
        GetComponent<AudioSource>().volume = 0.3f;
    }
}
