using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum STRUCTURE_TYPE
{
    Invalid,
    Wall,
    Room,
    Shop,
    Tile,
    ShopWall
}

public class TileGenerator : MonoBehaviour
{
    public Tile m_TilePrefab;
    public Wall m_WallPrefab;
    public Wall m_ShopWallPrefab;

    private Tile[,] m_TileMatrix;
    STRUCTURE_TYPE[,] m_MapMatrix;
    int[,] m_ReducedMatrix;
    private Tile m_StartingTile;
    int m_MatrixLength = 30 ;

    int m_ShopX;
    int m_ShopY;

    void Awake()
    {
        m_MapMatrix = new STRUCTURE_TYPE[m_MatrixLength, m_MatrixLength];
        m_TileMatrix = new Tile[m_MatrixLength, m_MatrixLength];

        GenerateMatrix();
        GenerateMap();
        SetTileNeighbour();
    }

    void GenerateMatrix()
    {
        SetupMatix();
        SetupShop();
        SetupRooms();
    }

    void SetupRooms()
    {
        int roomsNumber = Random.Range(5, 6);
        for (int k = 1; k <= roomsNumber; ++k)
        {
            bool roomOk = false;
            int roomHeigth = Random.Range(4, 6);
            int roomWidth = Random.Range(4, 7);

            int room_X = 0;
            int room_Y = 0;

            int trialCount = 0;

            while (roomOk == false)
            {
                room_X = Random.Range(5, m_MatrixLength - 5);
                room_Y = Random.Range(5, m_MatrixLength - 5);

                roomOk = true;
                for (int i = room_X - (roomHeigth - 1) / 2 - 1; i <= room_X + roomHeigth / 2 + 1; ++i)
                {
                    for (int j = room_Y - (roomHeigth - 1) / 2 - 1; j <= room_Y + roomHeigth / 2 + 1; ++j)
                    {
                        if (m_MapMatrix[i, j] != STRUCTURE_TYPE.Wall)
                        {
                            roomOk = false;
                        }
                    }
                }
                trialCount++;
                if (trialCount == 100)
                {
                    Debug.Log(trialCount);
                    roomOk = true;
                }
            }

            if (trialCount < 100)
            {
                for (int i = room_X - (roomHeigth - 1) / 2; i <= room_X + roomHeigth / 2; ++i)
                {
                    for (int j = room_Y - (roomHeigth - 1) / 2; j <= room_Y + roomHeigth / 2; ++j)
                    {
                        m_MapMatrix[i, j] = STRUCTURE_TYPE.Tile;
                    }
                }
            }
        }
    }
    /*
    void ReducedMatrix(int _rommSize)
    {
        m_ReducedMatrix = new int[m_MatrixLength / _rommSize, m_MatrixLength / _rommSize];
        for (int i = 0; i < m_MatrixLength; i+= _rommSize)
        {
            for (int j = 0; j < m_MatrixLength; j+= _rommSize)
            {
                for (int ii = i; ii < i + _rommSize; ++ii)
                {
                    for (int jj = j; jj < j + _rommSize; ++jj)
                    {
                        if (m_MapMatrix[ii, jj] != STRUCTURE_TYPE.Wall)
                        {
                            m_ReducedMatrix[i / _rommSize, j / _rommSize] = 1;
                        }
                    }
                }

                if(m_ReducedMatrix[i / _rommSize, j / _rommSize] != 1)
                {
                    m_ReducedMatrix[i / _rommSize, j / _rommSize] = 0;
                }
                Debug.Log(m_ReducedMatrix[i / _rommSize, j / _rommSize]);
            }
        }
    }
    */
    void SetupShop()
    {
        int min_range = 6;
        int max_range = m_MatrixLength - 6;

        int shop_X = Random.Range(min_range + 2, max_range + 2);
        int shop_Y = Random.Range(min_range, max_range);

        m_ShopX = shop_X;
        m_ShopY = shop_Y;


        for (int i = shop_X - 4; i <= shop_X + 4; ++i)
        {
            for (int j = shop_Y - 3; j <= shop_Y + 3; ++j)
            {
                m_MapMatrix[i, j] = STRUCTURE_TYPE.Tile;
                if (i == shop_X - 4 || i == shop_X + 4 || j == shop_Y - 3 || j == shop_Y + 3)
                {
                    m_MapMatrix[i, j] = STRUCTURE_TYPE.ShopWall;
                }
            }
        }
        m_MapMatrix[shop_X-4, shop_Y] = STRUCTURE_TYPE.Tile;
        m_MapMatrix[shop_X, shop_Y] = STRUCTURE_TYPE.Shop;
    }

    void SetupMatix()
    {
        for (int i = 0; i < m_MatrixLength; ++i)
        {
            for (int j = 0; j < m_MatrixLength; ++j)
            {
                m_MapMatrix[i,j] = STRUCTURE_TYPE.Wall;
            }
        }
    }

    void GenerateMap()
    {
        for (int i = 0; i < m_MatrixLength; i++)
        {
            for (int j = 0; j < m_MatrixLength; j++)
            {
                Vector3 position = new Vector3((float)j, (float)i, 20f);
                Tile prefab_tile = Instantiate(m_TilePrefab, position, Quaternion.identity);
                m_TileMatrix[i,j] = prefab_tile;
                prefab_tile.SetTile(TILE_DIRECTION.Up, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Down, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Left, prefab_tile);
                prefab_tile.SetTile(TILE_DIRECTION.Right, prefab_tile);

                prefab_tile.SetParity((i + j) % 2 == 0 ? BEAT_PARITY.Even : BEAT_PARITY.Odd);

                if (m_MapMatrix[i,j]==STRUCTURE_TYPE.Wall)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.5f, -10f + 0.1f * i);
                    CreateWall(m_WallPrefab, prefab_tile, position_wall, Quaternion.identity);
                }

                if (m_MapMatrix[i, j] == STRUCTURE_TYPE.ShopWall)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.5f, -10f + 0.1f * i);
                    CreateWall(m_ShopWallPrefab, prefab_tile, position_wall, Quaternion.identity);
                }

                if (i == m_ShopX && j == m_ShopY)
                {
                    m_StartingTile = prefab_tile;
                }
            }
        }
    }

    void SetTileNeighbour()
    {
        for (int i = 0; i < m_MatrixLength; i++)
        {
            for (int j = 0; j < m_MatrixLength; j++)
            {
                if (i > 0)
                    m_TileMatrix[i, j].SetTile(TILE_DIRECTION.Down, m_TileMatrix[i - 1, j]);
                if (j > 0)
                    m_TileMatrix[i, j].SetTile(TILE_DIRECTION.Left, m_TileMatrix[i, j - 1]);
                if (i < m_MatrixLength - 1)
                    m_TileMatrix[i, j].SetTile(TILE_DIRECTION.Up, m_TileMatrix[i + 1, j]);
                if (j < m_MatrixLength - 1)
                    m_TileMatrix[i, j].SetTile(TILE_DIRECTION.Right, m_TileMatrix[i, j + 1]);

            }
        }
    }

    void CreateWall(Wall _wallPrefab, Tile _tile, Vector3 _position, Quaternion _rotation)
    {
        Wall prefab_wall = Instantiate(_wallPrefab, _position, _rotation);
        _tile.AddToTile(prefab_wall.GetComponent<AboveTileObject>());
    }

    public Tile GetStartingTile()
    {
        return m_StartingTile;
    }
}
