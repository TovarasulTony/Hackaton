using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : GenericSoundReference
{
    [SerializeField]
    List<AudioClip> m_PlayerHit;

    void Awake()
    {
        m_AudioClipDictionary.Add("player_hit", m_PlayerHit);
    }
}
