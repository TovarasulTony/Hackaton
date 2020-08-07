using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : GenericBehavior, IPlayerSubscriber
{
    Player m_PlayerReference;
    Weapon m_Weapon;
    int m_Gold = 0;

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
            Equip(pickable.GetName());
            pickable.DestroyThis();
        }
    }

    public void Equip(string _weapon)
    {
        Unequip();
        m_Weapon = Library.instance.GetPattern(_weapon);
        UI.instance.NotifyUIChange("attack", _weapon);
    }

    public void PickGold(AboveTileObject _gold)
    {
        Gold gold = _gold.GetComponent<Gold>();
        m_Gold += gold.GetGold();
        SoundManager.instance.PlaySound("sound_effect", "pickup_gold");
        gold.DestroyThis();
    }

    void Unequip()
    {
        if(m_Weapon != null)
        {
            InstantiateWeapon(m_PlayerReference.GetTileReference(), m_Weapon.GetWeaponType());
        }
    }

    public List<KeyValuePair<int, int>> GetAttackPattern(DIRECTION _attackDirection)
    {
        return m_Weapon.GetAttackDictionary()[_attackDirection];
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
