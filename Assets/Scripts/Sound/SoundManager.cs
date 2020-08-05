using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("SoundManager instantiat de doua ori");
            Destroy(gameObject);
        }
        instance = GetComponent<SoundManager>();
    }

    public void PlaySound(string _category, string _subcategory)
    {
        switch(_category)
        {
            case "player":
                transform.Find("PlayerSounds").GetComponent<PlayerSounds>().Play(_subcategory);
                break;
            case "sound_effect":
                transform.Find("SoundEffects").GetComponent<SoundEffects>().Play(_subcategory);
                break;
        }
    }
}
