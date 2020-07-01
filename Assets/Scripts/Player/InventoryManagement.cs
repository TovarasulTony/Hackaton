using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : GenericBehavior, IPlayerSubscriber
{
    Player m_PlayerReference;

    InventoryManagement() { }

    public InventoryManagement(Player _reference)
    {
        m_PlayerReference = _reference;
        m_PlayerReference.Subscribe(this);
    }

    public void OnPlayerMovement(DIRECTION _direction)
    {
        AboveTileObject weapon = m_PlayerReference.GetTileReference().Contains<WeaponPickable>();
        if (weapon != null)
        {
            WeaponPickable pickable = weapon.GetComponent<WeaponPickable>();
            Weapon playerWeapon = m_PlayerReference.Equip(Library.instance.GetPattern(pickable.GetName()));
            pickable.DestroyThis();

            if(playerWeapon != null)
            {
                InstantiateWeapon(m_PlayerReference.GetTileReference(), playerWeapon.GetWeaponType());
            }
        }
    }

    void InstantiateWeapon(Tile _tile, string _weaponType)
    {
        WeaponPickable test_weapon = (Object.Instantiate(Library.instance.GetPrefab("WeaponPickable"))).GetComponent<WeaponPickable>();
        test_weapon.SetTileReference(_tile);
        test_weapon.SetWeaponType(_weaponType);
        _tile.AddToTile(test_weapon);
        Vector3 position = _tile.transform.position;
        test_weapon.transform.position = new Vector3(position.x, position.y + 0.5f, position.z - 0.1f);
    }
}
