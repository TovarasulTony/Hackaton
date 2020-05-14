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
    MapGenerator m_MapGenerator;
    MapInstantiator m_MapInstantiator;

    Dictionary<string, Tile> m_ImportantTiles;
    Dictionary<string, KeyValuePair<int, int>> m_SpecialCoordinates;

    STRUCTURE_TYPE[,] m_StructureMap;
    int[,] m_PlayerTrakingMap;

    int m_MatrixLength = 30;
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
        m_MapGenerator = new MapGenerator(GetComponent<Map>());
        m_MapInstantiator = new MapInstantiator(GetComponent<Map>());
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
}
