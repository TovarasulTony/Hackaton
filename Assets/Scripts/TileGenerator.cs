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
                prefab_tile.SetTile(TILE_DIRECTION.Up, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Down, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Left, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Right, prefab_tile);

                prefab_tile.SetParity((i + j) % 2 == 0 ? BEAT_PARITY.Even : BEAT_PARITY.Odd);

                if (i <= 2 || i >= 9 || j <= 2 || j >= 9)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.5f, -10f + 0.1f * i);
                    CreateWall(prefab_tile, position_wall, Quaternion.identity);
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
                    m_TileMatrix[i][j].SetTile(TILE_DIRECTION.Down, m_TileMatrix[i - 1][j]);
                if (j > 1)
                    m_TileMatrix[i][j].SetTile(TILE_DIRECTION.Left, m_TileMatrix[i][j - 1]);
                if (i < 10)
                    m_TileMatrix[i][j].SetTile(TILE_DIRECTION.Up, m_TileMatrix[i + 1][j]);
                if (j < 10)
                    m_TileMatrix[i][j].SetTile(TILE_DIRECTION.Right, m_TileMatrix[i][j + 1]);

            }
        }

    }

    void CreateWall(Tile _tile, Vector3 _position, Quaternion _rotation)
    {
        GameObject prefab = Instantiate(m_WallPrefab, _position, _rotation);
        Wall prefab_wall = prefab.GetComponent<Wall>();
        _tile.AddToTile(prefab_wall.gameObject);
    }
}
