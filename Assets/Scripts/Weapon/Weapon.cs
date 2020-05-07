using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    protected Dictionary<DIRECTION, List<KeyValuePair<int, int>>> m_AttackDictionary;

    public Weapon()
    {
        m_AttackDictionary = new Dictionary<DIRECTION, List<KeyValuePair<int, int>>>();
    }

    protected void CompleteAttackDictionary()
    {
        TranslateList(DIRECTION.Down);
        TranslateList(DIRECTION.Left);
        TranslateList(DIRECTION.Right);
    }

    protected void TranslateList(DIRECTION _direction)
    {
        List<KeyValuePair<int, int>> attackList = new List<KeyValuePair<int, int>>();
        foreach(KeyValuePair<int, int> pair in m_AttackDictionary[DIRECTION.Up])
        {
            switch(_direction)
            {
                case DIRECTION.Down:
                    attackList.Add(new KeyValuePair<int, int>(-pair.Key, -pair.Value));
                    break;
                case DIRECTION.Left:
                    attackList.Add(new KeyValuePair<int, int>(pair.Value, -pair.Key));
                    break;
                case DIRECTION.Right:
                    attackList.Add(new KeyValuePair<int, int>(-pair.Value, pair.Key));
                    break;
            }
        }
        m_AttackDictionary.Add(_direction, attackList);
    }

    public Dictionary<DIRECTION, List<KeyValuePair<int, int>>> GetAttackDictionary()
    {
        return m_AttackDictionary;
    }
}
