using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TileGenerator m_TileGenerator;
    private Tile m_CurrentTile;

    public float speed;

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

            m_CurrentTile = m_CurrentTile.m_TileDown;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up");

            m_CurrentTile = m_CurrentTile.m_TileUp;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left");

            m_CurrentTile = m_CurrentTile.m_TileLeft;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");

            m_CurrentTile = m_CurrentTile.m_TileRight;
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
    }
}
