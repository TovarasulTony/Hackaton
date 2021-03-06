﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : AboveTileObject
{
    public int m_WallMight;
    public void Dig(int _digMight)
    {
        if (_digMight >= m_WallMight)
        {
            m_CurrentTile.RemoveFromTile(GetComponent<Wall>());
            SoundManager.instance.PlaySound("sound_effect", "dig");
            SoundManager.instance.PlaySound("player", "dig");
            UI.instance.CameraShake();
            DestroyThis();
        }
    }
}
