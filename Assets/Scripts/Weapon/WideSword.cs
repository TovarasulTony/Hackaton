using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideSword : Weapon
{
    public WideSword()
    {
        List<KeyValuePair<int, int>> upList = new List<KeyValuePair<int, int>>();
        upList.Add(new KeyValuePair<int, int>(1, 0));
        upList.Add(new KeyValuePair<int, int>(1, -1));
        upList.Add(new KeyValuePair<int, int>(1, 1));
        m_AttackDictionary.Add(DIRECTION.Up, upList);

        CompleteAttackDictionary();
        m_WeaponType = "wide_sword";
    }
}
