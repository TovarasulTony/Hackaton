using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon_Pickable : MonoBehaviour
{
    Dictionary<string, Tile> m_TileList = null;

    private void Start()
    {
        m_TileList = new Dictionary<string, Tile>();

        Tile axeTile = Map.instance.GetTile("Starting");
        axeTile = axeTile.GetTile(DIRECTION.Right).GetTile(DIRECTION.Right).GetTile(DIRECTION.Up);
        m_TileList.Add("axe", axeTile);

        Tile wideSwordTile = Map.instance.GetTile("Starting");
        wideSwordTile = wideSwordTile.GetTile(DIRECTION.Left).GetTile(DIRECTION.Left).GetTile(DIRECTION.Up);
        m_TileList.Add("wide_sword", wideSwordTile);
    }

    void InstantiateWeapon(Tile _tile, string _weaponType)
    {
        WeaponPickable test_weapon= Instantiate(Library.instance.GetPrefab("WeaponPickable")).GetComponent<WeaponPickable>();
        test_weapon.SetTileReference(_tile);
        test_weapon.SetWeaponType(_weaponType);
        _tile.AddToTile(test_weapon);
        Vector3 position = _tile.transform.position;
        test_weapon.transform.position = new Vector3(position.x, position.y + 0.5f, position.z - 0.1f);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            InstantiateWeapon(m_TileList["axe"], "axe");
            InstantiateWeapon(m_TileList["wide_sword"], "wide_sword");
        }
    }
}

