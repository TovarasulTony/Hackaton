using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STRUCTURE_TYPE
{
    Invalid,
    Wall,
    Room,
    Shop,
    Tile,
    ShopWall
}

public class Map : MonoBehaviour
{
    public static Map instance = null;
    Pathfinder m_Pathfinder;
    MapGenerator m_MapGenerator;
    MapInstantiator m_MapInstantiator;

    Dictionary<string, Tile> m_ImportantTiles;
    Dictionary<string, KeyValuePair<int, int>> m_SpecialCoordinates;

    STRUCTURE_TYPE[,] m_StructureMap;
    int[,] m_PlayerTrakingMap;
    Tile[,] m_TileMatrix;

    int m_MatrixLength = 90;
    private Tile m_StartingTile;
    private Tile m_EnemyTile;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("BeatMaster instantiat de doua ori");
            Destroy(gameObject);
        }
        instance = gameObject.GetComponent<Map>();
        m_ImportantTiles = new Dictionary<string, Tile>();
        m_SpecialCoordinates = new Dictionary<string, KeyValuePair<int, int>>();
        m_MapGenerator = new MapGenerator();
        m_MapInstantiator = new MapInstantiator();
        m_Pathfinder = new Pathfinder();
    }

    public int GetMatrixLength()
    {
        return m_MatrixLength;
    }

    public void SetStructureMap(STRUCTURE_TYPE[,] _map)
    {
        m_StructureMap = _map;
    }

    public STRUCTURE_TYPE[,] GetStructureMap()
    {
        return m_StructureMap;
    }

    public void AddSpecialTile(string _tileName, Tile _tile)
    {
        m_ImportantTiles.Add(_tileName, _tile);
    }

    public void AddCoordinates(string _tileName, KeyValuePair<int, int> _coordinates)
    {
        m_SpecialCoordinates.Add(_tileName, _coordinates);
    }

    public KeyValuePair<int, int> GetCoordinates(string _name)
    {
        return m_SpecialCoordinates[_name];
    }

    public Tile GetTile(string _tileName)
    {
        return m_ImportantTiles[_tileName];
    }

    public void SetPlayerReference(Player _player)
    {
        m_Pathfinder.SetPlayerReference(_player);
    }

    public void SetTileMatrix(Tile[,] _matrix)
    {
        m_TileMatrix = _matrix;
    }

    public int GetPlayerDistance(Tile _tile)
    {
        return m_Pathfinder.GetPlayerDistance(_tile);
    }
}
