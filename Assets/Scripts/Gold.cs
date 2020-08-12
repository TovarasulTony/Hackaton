using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : AboveTileObject
{
    int m_Gold;

    public void SetGold(int _gold)
    {
        m_Gold = _gold;
    }

    public int GetGold()
    {
        return m_Gold;
    }
}
