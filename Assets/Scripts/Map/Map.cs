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
    GameObject m_EnemiesObject;

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
        m_EnemiesObject = new GameObject("Enemies");
        instance = gameObject.GetComponent<Map>();
        m_ImportantTiles = new Dictionary<string, Tile>();
        m_SpecialCoordinates = new Dictionary<string, KeyValuePair<int, int>>();
        m_MapGenerator = new MapGenerator();
        m_MapInstantiator = new MapInstantiator();
        m_Pathfinder = new Pathfinder();
        m_TileMatrix = m_MapInstantiator.GetTileMatrix(); 
        List<RoomStruct> roomList = m_MapGenerator.GetRoomList();
        GenerateMonsters(roomList);
    }

    void GenerateMonsters(List<RoomStruct> _roomList)
    {
        foreach (RoomStruct room in _roomList)
        {
            int numberOfEnemies = Random.Range(2, 4);
            for (int i = 1; i <= numberOfEnemies; ++i)
            {
                int x = room.x + Random.Range(-(room.heigth - 1) / 2, (room.heigth - 1) / 2 + 1);
                int y = room.y + Random.Range(-(room.width - 1) / 2, (room.width - 1) / 2 + 1);

                if(m_TileMatrix[x, y].Contains<Enemy>() || m_TileMatrix[x, y].Contains<Player>())
                {
                    continue;
                }
                Enemy enemy = Instantiate(GetComponent<MapReferences>().m_EnemyList[Random.Range(0, GetComponent<MapReferences>().m_EnemyList.Count)]);
                enemy.transform.parent = m_EnemiesObject.transform;
                m_TileMatrix[x, y].AddToTile(enemy);
            }
        }
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

    public Tile GetTile(int _x, int _y)
    {
        return m_TileMatrix[_x, _y];
    }

    public void AddEnemy(Enemy _enemy)
    {
        m_Pathfinder.AddEnemy(_enemy);
    }

    public void RemoveEnemy(Enemy _enemy)
    {
        m_Pathfinder.RemoveEnemy(_enemy);
    }

    public bool IsTileBlocked(Tile _tile)
    {
        return m_Pathfinder.IsTileBlocked(_tile);
    }
}
