using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AboveTileObject
{
    private TileGenerator m_TileGenerator;
    List<IPlayerSubscriber> m_SubscriberList;

    void Start()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = m_TileGenerator.GetStartingTile();
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Down));
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Up));
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Left));
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Right));
        }
    }

    void Moving(Tile _nextTile)
    {
        if (_nextTile.Contains<Wall>() != null)
        {
            _nextTile.Contains<Wall>().GetComponent<Wall>().Dig(1);
        }
        else
        {
            m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
            m_CurrentTile = _nextTile;
            m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
        NotifySubscribers();
    }

    public void Subscribe(IPlayerSubscriber _subscriber)
    {
        if(m_SubscriberList == null)
        {
            m_SubscriberList = new List<IPlayerSubscriber>();
        }
        m_SubscriberList.Add(_subscriber);
    }

    void NotifySubscribers()
    {
        if(m_SubscriberList == null)
        {
            return;
        }

        foreach(IPlayerSubscriber subscriber in m_SubscriberList)
        {
            subscriber.OnPlayerMovement();
        }
    }

    public void SetTileGenerator(TileGenerator _generatorReference)
    {
        m_TileGenerator = _generatorReference;
    }
}

