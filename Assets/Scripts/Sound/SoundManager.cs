using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    Dictionary<string, Dictionary<string, List<AudioClip>>> m_AudioClips;
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("SoundManager instantiat de doua ori");
            Destroy(gameObject);
        }
        instance = GetComponent<SoundManager>();
        m_AudioClips = new Dictionary<string, Dictionary<string, List<AudioClip>>>();
        Dictionary<string, List<AudioClip>> playerClips = new Dictionary<string, List<AudioClip>>();
        playerClips.Add("mele", transform.Find("PlayerSounds").GetComponent<PlayerSounds>().GetClipList("mele"));
        playerClips.Add("dig", transform.Find("PlayerSounds").GetComponent<PlayerSounds>().GetClipList("dig"));
        m_AudioClips.Add("player", playerClips);
    }

    public void PlayerSound(string _category, string _subcategory)
    {
        AudioSource audio = GetComponent<AudioSource>();
        List<AudioClip> audioList = m_AudioClips[_category][_subcategory];
        Debug.Log(audio.volume);
        audio.clip = audioList[Random.Range(0, audioList.Count)];
        audio.Play(0);
    }
}
