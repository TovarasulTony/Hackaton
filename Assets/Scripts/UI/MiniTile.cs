using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniTile : MonoBehaviour , IBeat
{
    MINITILE_STATUS m_MiniTileStatus;
    Tile m_Tile;
    SpriteRenderer m_SpriteRenderer;
    Color DirtWallColor = new Color(0.54f, 0.27f, 0.07f, 1);
    Color FloorColor = new Color(0.87f, 0.72f, 0.52f, 1);
    Color BrickWallColor = new Color(1, 0.84f, 0, 1);
    Color PlayerColor = new Color(0, 0, 1, 1);
    Color EnemyColor = new Color(1, 0, 0, 1);

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

            if (m_Tile.GetNumberOfElements() == 0)
                spriteRenderer.color = FloorColor;
            else if (m_Tile.Contains<Wall>() != null)
            {
                if (m_MiniTileStatus == MINITILE_STATUS.DirtWall)
                    spriteRenderer.color = DirtWallColor;
                else
                    spriteRenderer.color = BrickWallColor;
            }
            else if (m_Tile.Contains<Enemy>() != null)
                spriteRenderer.color = EnemyColor;
            else if (m_Tile.Contains<Player>() != null)
                spriteRenderer.color = PlayerColor;
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
