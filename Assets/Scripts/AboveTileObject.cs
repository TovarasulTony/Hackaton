using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveTileObject : MonoBehaviour
{
    Tile m_TileReference = null;

    public void SetTileReference(Tile _tile)
    {
        m_TileReference = _tile;
    }

    public void DestroyThis()
    {
        m_TileReference.RemoveFromTile(gameObject.GetComponent<AboveTileObject>());
        Destroy(gameObject);
    }
}
