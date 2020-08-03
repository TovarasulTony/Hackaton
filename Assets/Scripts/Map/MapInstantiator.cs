using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstantiator
{
    public Tile m_TilePrefab;
    public Wall m_WallPrefab;
    public List<Sprite> m_WallSpriteListT1;
    public List<Sprite> m_WallSpriteListT2;
    public List<Sprite> m_WallSpriteListT3;
    public Wall m_ShopWallPrefab;

    int m_MatrixLength;
    int m_ShopX=40;
    int m_ShopY=50;
    GameObject m_MapGameObject;
    private Tile[,] m_TileMatrix;
    private Tile m_StartingTile;
    private Tile m_EnemyTile;


    public MapInstantiator()
    {
        m_MatrixLength = Map.instance.GetMatrixLength();
        m_MapGameObject = new GameObject("Map");
        m_TileMatrix = new Tile[m_MatrixLength, m_MatrixLength];

        SetupReferences();
        InstantiateMap(Map.instance.GetStructureMap());
        SetTileNeighbour();

        Map.instance.AddSpecialTile("Starting", m_StartingTile);
        Map.instance.AddSpecialTile("Enemy", m_EnemyTile);
    }

    void SetupReferences()
    {
        MapReferences spriteLocation = Map.instance.GetComponent<MapReferences>();
        m_TilePrefab = spriteLocation.m_TilePrefab;
        m_WallPrefab = spriteLocation.m_WallPrefab;
        m_WallSpriteListT1 = spriteLocation.m_WallSpriteListT1;
        m_WallSpriteListT2 = spriteLocation.m_WallSpriteListT2;
        m_WallSpriteListT3 = spriteLocation.m_WallSpriteListT3;
        m_ShopWallPrefab = spriteLocation.m_ShopWallPrefab;

        //KeyValuePair<int, int> shoopCoordinates = Map.instance.GetCoordinates("Shop");
        //m_ShopX = shoopCoordinates.Key;
        //m_ShopY = shoopCoordinates.Value;
    }

    void InstantiateMap(STRUCTURE_TYPE[,] _mapMatrix)
    {
        for (int i = 0; i < m_MatrixLength; i++)
        {
            for (int j = 0; j < m_MatrixLength; j++)
            {
                if (_mapMatrix[i, j] == STRUCTURE_TYPE.Invalid)
                {
                    continue;
                }
                Vector3 position = new Vector3((float)j, (float)i, 100f);
                Tile prefab_tile = Object.Instantiate(m_TilePrefab, position, Quaternion.identity);
                prefab_tile.transform.parent = m_MapGameObject.transform;
                m_TileMatrix[i, j] = prefab_tile;
                prefab_tile.SetTile(DIRECTION.Up, prefab_tile);
                prefab_tile.SetTile(DIRECTION.Down, prefab_tile);
                prefab_tile.SetTile(DIRECTION.Left, prefab_tile);
                prefab_tile.SetTile(DIRECTION.Right, prefab_tile);
                prefab_tile.SetCoordinates(i, j);
                prefab_tile.SetLayerNumber(i);

                prefab_tile.SetParity((i + j) % 2 == 0 ? BEAT_PARITY.Even : BEAT_PARITY.Odd);

                if (_mapMatrix[i, j] == STRUCTURE_TYPE.Wall)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.5f, prefab_tile.GetLayerNumber());
                    CreateWall(prefab_tile, position_wall, Quaternion.identity);
                }

                if (_mapMatrix[i, j] == STRUCTURE_TYPE.ShopWall)
                {
                    Vector3 position_wall = new Vector3((float)j, (float)i + 0.5f, prefab_tile.GetLayerNumber());
                    CreateWall(prefab_tile, position_wall, Quaternion.identity, m_ShopWallPrefab);
                }

                if (i == m_ShopX + 3 && j == m_ShopY)
                {
                    m_EnemyTile = prefab_tile;
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
                if (m_TileMatrix[i, j] == null)
                    continue;
                if (i > 0)
                    m_TileMatrix[i, j].SetTile(DIRECTION.Down, m_TileMatrix[i - 1, j]);
                if (j > 0)
                    m_TileMatrix[i, j].SetTile(DIRECTION.Left, m_TileMatrix[i, j - 1]);
                if (i < m_MatrixLength - 1)
                    m_TileMatrix[i, j].SetTile(DIRECTION.Up, m_TileMatrix[i + 1, j]);
                if (j < m_MatrixLength - 1)
                    m_TileMatrix[i, j].SetTile(DIRECTION.Right, m_TileMatrix[i, j + 1]);

            }
        }
    }

    void CreateWall(Tile _tile, Vector3 _position, Quaternion _rotation, Wall _wallPrefab = null)
    {
        Wall prefab_wall = null;
        if (_wallPrefab != null)
        {
            prefab_wall = Object.Instantiate(_wallPrefab, _position, _rotation);
        }
        else
        {
            prefab_wall = Object.Instantiate(m_WallPrefab, _position, _rotation);
            SpriteRenderer wallSpriteRenderer = prefab_wall.GetComponent<SpriteRenderer>();
            List<Sprite> spriteList;
            int rng = Random.Range(1, 100);
            if (rng < 81)
            {
                spriteList = m_WallSpriteListT1;
            }
            else if (rng < 96)
            {
                spriteList = m_WallSpriteListT2;
            }
            else
            {
                spriteList = m_WallSpriteListT3;
            }
            wallSpriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
        }

        prefab_wall.transform.parent = m_MapGameObject.transform;
        _tile.AddToTile(prefab_wall.GetComponent<AboveTileObject>());
    }

    public Tile[,] GetTileMatrix()
    {
        return m_TileMatrix;
    }
}
