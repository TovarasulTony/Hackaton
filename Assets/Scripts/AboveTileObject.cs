using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveTileObject : GenericActor
{
    protected Tile m_CurrentTile = null;
    public Sprite m_UnexploredSprite;
    public FOG_STATUS m_FogStatus;

    public void SetFogStatus(FOG_STATUS _status)
    {
        m_FogStatus = _status;
        if(GetComponent<SpriteRenderer>() == null)
        {
            return;
        }
        switch (m_FogStatus)
        {
            case FOG_STATUS.Unexplored:
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                break;
            case FOG_STATUS.Explored:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case FOG_STATUS.InVision:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                break;
        }
    }

    public void SetTileReference(Tile _tile)
    {
        m_CurrentTile = _tile;
    }

    public Tile GetTileReference()
    {
        return m_CurrentTile;
    }

    public void DestroyThis()
    {
        m_CurrentTile.RemoveFromTile(gameObject.GetComponent<AboveTileObject>());
        Destroy(gameObject);
    }
}
