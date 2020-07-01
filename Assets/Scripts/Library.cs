using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    [SerializeField]
    List<Sprite> m_WeaponSpriteList;
    [SerializeField]
    List<Transform> m_PrefabList;

    Dictionary<string, Sprite> m_WeaponSpriteDictionary;
    Dictionary<string, Weapon> m_WeaponPatternDictionary;
    Dictionary<string, Transform> m_PrefabDictionary;

    static public Library instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Library instantiated twice!!!");
            Destroy(instance);
            return;
        }
        instance = GetComponent<Library>();
        m_PrefabDictionary = new Dictionary<string, Transform>();
        m_WeaponSpriteDictionary = new Dictionary<string, Sprite>();
        m_WeaponPatternDictionary = new Dictionary<string, Weapon>();

        foreach (Sprite sprite in m_WeaponSpriteList)
        {
            m_WeaponSpriteDictionary.Add(sprite.name, sprite);
        }
        foreach (Transform prefab in m_PrefabList)
        {
            m_PrefabDictionary.Add(prefab.name, prefab);
        }


        //to be completed
        m_WeaponPatternDictionary.Add("axe", new Axe());
        m_WeaponPatternDictionary.Add("wide_sword", new WideSword());
    }

    public Sprite GetSprite(string _sprite)
    {
        Debug.Log(_sprite);
        return m_WeaponSpriteDictionary[_sprite];
    }

    public Weapon GetPattern(string _pattern)
    {
        return m_WeaponPatternDictionary[_pattern];
    }

    public Transform GetPrefab(string _prefab)
    {
        return m_PrefabDictionary[_prefab];
    }
}
