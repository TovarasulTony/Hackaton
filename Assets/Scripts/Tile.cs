using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_DIRECTION
{
    Invalid,
    Up,
    Down,
    Left,
    Right
}

public class Tile : MonoBehaviour, IBeat
{
    public Sprite m_LightSprite;
    public Sprite m_DarkSprite;
    Dictionary<TILE_DIRECTION, Tile> m_Tiles = null;
    List<GameObject> m_ContainedObjects = null;
    BEAT_PARITY m_TileParity;

    // Start is called before the first frame update
    void Awake()
    {
        m_ContainedObjects = new List<GameObject>();
        m_Tiles = new Dictionary<TILE_DIRECTION, Tile>();
        m_Tiles.Add(TILE_DIRECTION.Up, null);
        m_Tiles.Add(TILE_DIRECTION.Down, null);
        m_Tiles.Add(TILE_DIRECTION.Left, null);
        m_Tiles.Add(TILE_DIRECTION.Right, null);

        BeatMaster.instance.SubscribeToBeat(gameObject.GetComponent<IBeat>());
    }

    public void AddToTile(GameObject _objectToAdd)
    {
        m_ContainedObjects.Add(_objectToAdd);
    }

    public void RemoveFromTile(GameObject _objetToRemove)
    {
        if(m_ContainedObjects.Contains(_objetToRemove))
        {
            m_ContainedObjects.Remove(_objetToRemove);
        }
    }

    public GameObject Contains<T>()
    {
        foreach(GameObject element in m_ContainedObjects)
        {
            if(element.GetComponent<T>() != null)
            {
                return element;
            }
        }
        return null;
    }

    public void SetTile(TILE_DIRECTION _tileDirection, Tile _tile)
    {
        m_Tiles[_tileDirection] = _tile;
    }

    public Tile GetTile(TILE_DIRECTION _tileDirection)
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
