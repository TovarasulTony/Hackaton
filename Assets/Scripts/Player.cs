using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
            Debug.Log("Down");

            Moving(m_CurrentTile.m_TileDown);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            Moving(m_CurrentTile.m_TileUp);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left");

            Moving(m_CurrentTile.m_TileLeft);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");

            Moving(m_CurrentTile.m_TileRight);
        }
    }
    void Moving(Tile _nextTile)
    {
        if (_nextTile.m_Wall != null)
        {
            Debug.Log("cv");
            _nextTile.m_Wall.Dig();
        }
        else
        {
            m_CurrentTile = _nextTile;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }


    }
}

