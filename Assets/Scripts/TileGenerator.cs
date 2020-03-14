using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tile;
    private Tile[][] m_TileMatrix=new Tile[20][];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            Tile[] tile_array = new Tile[20];
            for (int j = 1; j <= 10; j++)
            {
                GameObject prefab;
                Vector3 position = new Vector3((float)j,(float)i, 0f);
                prefab=Instantiate(tile, position, Quaternion.identity);

                Tile prefab_tile = prefab.GetComponent<Tile>();
                tile_array[j] = prefab_tile;
                prefab_tile.m_TileUp = prefab_tile;
                prefab_tile.m_TileDown = prefab_tile;
                prefab_tile.m_TileLeft = prefab_tile;
                prefab_tile.m_TileRight = prefab_tile;

            }
            m_TileMatrix[i] = tile_array;
        }

        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                if (i > 1)
                    m_TileMatrix[i][j].m_TileUp = m_TileMatrix[i-1][j];
                if (j > 1)
                    m_TileMatrix[i][j].m_TileLeft = m_TileMatrix[i][j-1];
                if (i < 20)
                    m_TileMatrix[i][j].m_TileDown = m_TileMatrix[i+1][j];
                if (j <20)
                    m_TileMatrix[i][j].m_TileRight = m_TileMatrix[i][j+1];

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
