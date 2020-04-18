using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AboveTileObject
{
    public TileGenerator m_TileGenerator;
    private Tile m_CurrentTile;

    void Start()
    {
        m_CurrentTile = m_TileGenerator.m_StartingTile;
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
            m_CurrentTile = _nextTile;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
    }
}

