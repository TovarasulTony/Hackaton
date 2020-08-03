using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> m_MeleSounds;
    [SerializeField]
    List<AudioClip> m_DigSounds;
    public List<AudioClip> GetClipList(string _list)
    {
        switch(_list)
        {
            case "mele":
                return m_MeleSounds;
            case "dig":
                return m_DigSounds;
        }
        return null;
    }
}
