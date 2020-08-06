using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    public Dagger()
    {
        List<KeyValuePair<int, int>> upList = new List<KeyValuePair<int, int>>();
        upList.Add(new KeyValuePair<int, int>(1, 0));
        m_AttackDictionary.Add(DIRECTION.Up, upList);

        CompleteAttackDictionary();
        m_WeaponType = "dagger";
    }
}
