using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniTile : MonoBehaviour , IBeat
{
    public Sprite m_Sprite;
    MINITILE_STATUS m_MiniTileStatus;
    Tile m_Tile;
    SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        m_MiniTileStatus = MINITILE_STATUS.Invalid;
        BeatMaster.instance.SubscribeToBeat(gameObject.GetComponent<IBeat>());
    }

    public void OnBeat()
    {
        if (m_Tile.m_FogStatus == FOG_STATUS.InVision)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            
            if(m_Tile.GetNumberOfElements() == 0)
                spriteRenderer.color = new Color(0.749f, 0.356f, 0.250f, 1);
            else if (m_Tile.Contains<Wall>() != null)
            {
                if(m_MiniTileStatus == MINITILE_STATUS.DirtWall)
                    spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
                else
                    spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1);
            }
            else if (m_Tile.Contains<Enemy>() != null)
                spriteRenderer.color = new Color(1, 0, 0, 1);
            else if (m_Tile.Contains<Player>() != null)
                spriteRenderer.color = new Color(0, 0, 1, 1);
        }
    }

    public MINITILE_STATUS GetMiniTileStatus()
    {
        return m_MiniTileStatus;
    }

    public void SetMiniTileStatus(MINITILE_STATUS _status)
    {
        m_MiniTileStatus = _status;
    }

    public void SetTile(Tile _tile)
    {
        m_Tile = _tile;
    }

    public Tile GetTile()
    {
        return m_Tile;
    }

}
