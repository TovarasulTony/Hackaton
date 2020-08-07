using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : GenericSoundReference
{
    [SerializeField]
    List<AudioClip> m_PlayerHit;
    [SerializeField]
    List<AudioClip> m_GoldPickup;

    void Awake()
    {
        m_AudioClipDictionary.Add("player_hit", m_PlayerHit);
        m_AudioClipDictionary.Add("pickup_gold", m_GoldPickup);
    }
}
