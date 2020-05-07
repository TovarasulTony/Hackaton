using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IBeat
{
    public Sprite m_LightSprite;
    public Sprite m_DarkSprite;
    Dictionary<DIRECTION, Tile> m_Tiles = null;
    List<AboveTileObject> m_ContainedObjects = null;
    BEAT_PARITY m_TileParity;
    int m_Elevation = -1;

    // Start is called before the first frame update
    void Awake()
    {
        m_ContainedObjects = new List<AboveTileObject>();
        m_Tiles = new Dictionary<DIRECTION, Tile>();
        m_Tiles.Add(DIRECTION.Up, null);
        m_Tiles.Add(DIRECTION.Down, null);
        m_Tiles.Add(DIRECTION.Left, null);
        m_Tiles.Add(DIRECTION.Right, null);

        BeatMaster.instance.SubscribeToBeat(gameObject.GetComponent<IBeat>());
    }

    public void AddToTile(AboveTileObject _objectToAdd)
    {
        m_ContainedObjects.Add(_objectToAdd);
        _objectToAdd.SetTileReference(gameObject.GetComponent<Tile>());
    }

    public void RemoveFromTile(AboveTileObject _objetToRemove)
    {
        if(m_ContainedObjects.Contains(_objetToRemove))
        {
            m_ContainedObjects.Remove(_objetToRemove);
        }
    }

    public AboveTileObject Contains<T>()
    {
        foreach(AboveTileObject element in m_ContainedObjects)
        {
            if(element.GetComponent<T>() != null)
            {
                return element;
            }
        }
        return null;
    }

    public void SetTile(DIRECTION _tileDirection, Tile _tile)
    {
        m_Tiles[_tileDirection] = _tile;
    }

    public Tile GetTile(DIRECTION _tileDirection)
    {
        return m_Tiles[_tileDirection];
    }

    public void SetParity(BEAT_PARITY _tileParity)
    {
        m_TileParity = _tileParity;
    }

    public void OnBeat()
    {
        BEAT_PARITY beatparity = BeatMaster.instance.GetBeatParity();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if(beatparity == m_TileParity)
        {
            spriteRenderer.sprite = m_DarkSprite;
        }
        else
        {
            spriteRenderer.sprite = m_LightSprite;
        }
    }
}
