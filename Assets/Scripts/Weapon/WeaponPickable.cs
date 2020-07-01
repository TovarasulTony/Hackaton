using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : AboveTileObject
{
    string m_WeaponType = "";

    WeaponPickable() { }

    public WeaponPickable(string _weaponType, Tile _belowTile)
    {
        m_WeaponType = _weaponType;
    }

    void Start()
    {
        //SetWeaponType("axe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeaponType(string _type)
    {
        m_WeaponType = _type;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = Library.instance.GetSprite(_type);
        //transform.localScale = new Vector3(4, 4, 4);
    }

    public string GetName()
    {
        return m_WeaponType;
    }
}
