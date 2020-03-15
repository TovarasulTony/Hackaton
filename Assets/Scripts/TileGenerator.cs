using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject m_TilePrefab;
    public GameObject m_WallPrefab;

    private Tile[][] m_TileMatrix=new Tile[20][];
    public Tile m_StartingTile;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 1; i <= 10; i++)
        {
            Tile[] tile_array = new Tile[20];
            for (int j = 1; j <= 10; j++)
            {
                GameObject prefab;
                Vector3 position = new Vector3((float)j,(float)i, 20f);
                

                
                prefab = Instantiate(m_TilePrefab, position, Quaternion.identity);
                Tile prefab_tile = prefab.GetComponent<Tile>();
                tile_array[j] = prefab_tile;
                prefab_tile.m_TileUp = prefab_tile;
                prefab_tile.m_TileDown = prefab_tile;
                prefab_tile.m_TileLeft = prefab_tile;
                prefab_tile.m_TileRight = prefab_tile;

                if (i <= 2 || i >= 9 || j <= 2 || j >= 9)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.25f, -10f + 0.1f * i);
                    prefab = Instantiate(m_WallPrefab, position_wall, Quaternion.identity);
                    Wall prefab_wall = prefab.GetComponent<Wall>();
                    prefab_tile.m_Wall = prefab_wall;
                }

                if (i==5 && j == 5)
                {
                    m_StartingTile = prefab_tile;
                }
            }
            m_TileMatrix[i] = tile_array;
        }


        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                if (i > 1)
                    m_TileMatrix[i][j].m_TileDown = m_TileMatrix[i - 1][j];
                if (j > 1)
                    m_TileMatrix[i][j].m_TileLeft = m_TileMatrix[i][j-1];
                if (i < 10)
                    m_TileMatrix[i][j].m_TileUp = m_TileMatrix[i + 1][j];
                if (j < 10)
                    m_TileMatrix[i][j].m_TileRight = m_TileMatrix[i][j+1];

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
