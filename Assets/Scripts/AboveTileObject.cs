using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveTileObject : MonoBehaviour
{
    protected Tile m_CurrentTile = null;

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
