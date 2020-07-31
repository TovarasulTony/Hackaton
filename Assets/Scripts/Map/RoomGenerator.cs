using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    List<RoomStruct> m_RoomsList;
    RoomStruct[] m_RoomsArray;
    Dictionary<RoomStruct, RoomStruct> m_MinRoom;
    STRUCTURE_TYPE[,] m_MapMatrix;
    int m_MatrixLength;

    public RoomGenerator(ref STRUCTURE_TYPE[,] _mapMatrix, int _matrixLength)
    {
        m_MapMatrix = _mapMatrix;
        m_MatrixLength = _matrixLength;
    }

    public void GenerateRooms()
    {
        List<RoomStruct> first_line = new List<RoomStruct>();
        List<RoomStruct> second_line = new List<RoomStruct>();
        List<RoomStruct> third_line = new List<RoomStruct>();

        first_line.Add(new RoomStruct());
        second_line.Add(new RoomStruct());
        third_line.Add(new RoomStruct());
        int numOfRooms = Random.Range(5, 7);
        for (int i = 4; i <= numOfRooms; ++i)
        {
            int assignedRow = Random.Range(1, 4);
            switch (assignedRow)
            {
                case 1:
                    first_line.Add(new RoomStruct());
                    break;
                case 2:
                    second_line.Add(new RoomStruct());
                    break;
                case 3:
                    third_line.Add(new RoomStruct());
                    break;
                default:
                    Debug.LogWarning("Something went very wrong!");
                    break;
            }
        }
        KeyValuePair<int, int> first_second_link = new KeyValuePair<int, int>(Random.Range(0, first_line.Count), Random.Range(0, second_line.Count));
        KeyValuePair<int, int> second_third_link = new KeyValuePair<int, int>(Random.Range(0, second_line.Count), Random.Range(0, third_line.Count));
        first_line[0].x = 40;
        first_line[0].y = 50;
        MarkRoomOnMatrix(first_line[0]);
        for (int i = 1; i < first_line.Count; ++i)
        {
            first_line[i].x = first_line[0].x + Random.Range(-3, 4);
            first_line[i].y = first_line[i - 1].y + Random.Range(6, 10);
            MarkRoomOnMatrix(first_line[i]);
            MakePath(first_line[i - 1], first_line[i]);
        }
        generate_row(ref first_line, ref second_line, first_second_link);
        generate_row(ref second_line, ref third_line, second_third_link);
        bool isNeighborUp = Random.Range(1, 3) == 1 ? true : false;
        List<RoomStruct> room_list = isNeighborUp == true ? first_line : third_line;
        RoomStruct shopNeighbor = room_list[Random.Range(0, room_list.Count)];
        RoomStruct shop = new RoomStruct();
        shop.y = shopNeighbor.y;
        shop.x = shopNeighbor.x + (isNeighborUp == true ? 7 : -7);
        MarkShop(shop);
        MakePath(shop, shopNeighbor);
        TrimMap(first_line, second_line, third_line);
    }

    void MarkShop(RoomStruct _shop)
    {
        for (int i = _shop.x - 4; i <= _shop.x + 4; ++i)
        {
            for (int j = _shop.y - 3; j <= _shop.y + 3; ++j)
            {
                m_MapMatrix[i, j] = STRUCTURE_TYPE.Tile;
                if (i == _shop.x - 4 || i == _shop.x + 4 || j == _shop.y - 3 || j == _shop.y + 3)
                {
                    m_MapMatrix[i, j] = STRUCTURE_TYPE.ShopWall;
                }
            }
        }
    }

    void generate_row(ref List<RoomStruct> _upper_line, ref List<RoomStruct> _lower_line, KeyValuePair<int, int> _link)
    {
        _lower_line[_link.Value].x = _upper_line[_link.Key].x - Random.Range(6, 10);
        _lower_line[_link.Value].y = _upper_line[_link.Key].y;
        MarkRoomOnMatrix(_lower_line[_link.Value]);
        MakePath(_upper_line[_link.Key], _lower_line[_link.Value]);
        for (int i = _link.Value + 1; i < _lower_line.Count; ++i)
        {
            _lower_line[i].x = _lower_line[_link.Value].x + Random.Range(-3, 4);
            _lower_line[i].y = _lower_line[i - 1].y + Random.Range(6, 10);
            MarkRoomOnMatrix(_lower_line[i]);
        }
        for (int i = _link.Value - 1; i >= 0; --i)
        {
            _lower_line[i].x = _lower_line[_link.Value].x + Random.Range(-3, 4);
            _lower_line[i].y = _lower_line[i + 1].y - Random.Range(6, 10);
            MarkRoomOnMatrix(_lower_line[i]);
        }
        for (int i = 1; i < _lower_line.Count; ++i)
        {
            MakePath(_lower_line[i - 1], _lower_line[i]);
        }
    }

    void MarkRoomOnMatrix(RoomStruct _room)
    {

        for (int i = _room.x - (_room.heigth - 1) / 2; i <= _room.x + _room.heigth / 2; ++i)
        {
            for (int j = _room.y - (_room.width - 1) / 2; j <= _room.y + _room.width / 2; ++j)
            {
                m_MapMatrix[i, j] = STRUCTURE_TYPE.Tile;
            }
        }
    }

    void MakePath(RoomStruct _first, RoomStruct _second)
    {
        int direction = _first.y - _second.y > 0 ? -1 : 1;
        for (int i = _first.y; i != _second.y + direction; i = i + direction)
        {
            m_MapMatrix[_first.x, i] = STRUCTURE_TYPE.Tile;
        }
        direction = _first.x - _second.x > 0 ? -1 : 1;
        for (int i = _first.x; i != _second.x + direction; i = i + direction)
        {
            m_MapMatrix[i, _second.y] = STRUCTURE_TYPE.Tile;
        }
    }
    void TrimMap(List<RoomStruct> _first_line, List<RoomStruct> _second_line, List<RoomStruct> _third_line)
    {
        int min_y = _first_line[0].y;
        int max_y = _first_line[0].y;
        int min_x = _first_line[0].x;
        int max_x = _first_line[0].x;
        FindMinValues(ref min_y, ref max_y, ref min_x, ref max_x, _first_line);
        FindMinValues(ref min_y, ref max_y, ref min_x, ref max_x, _second_line);
        FindMinValues(ref min_y, ref max_y, ref min_x, ref max_x, _third_line);

        Debug.Log(min_y + " " + max_y + " " + min_x + " " + max_x);
        for (int i = 0; i < m_MatrixLength; i++)
        {
            for (int j = 0; j < m_MatrixLength; j++)
            {
                if (i < min_x - 12 || i > max_x + 12 || j < min_y - 12 || j > max_y + 12)
                {
                    Debug.Log(i + " " + j);
                    m_MapMatrix[i, j] = STRUCTURE_TYPE.Invalid;
                }
            }
        }
    }

    void FindMinValues(ref int min_y, ref int max_y, ref int min_x, ref int max_x, List<RoomStruct> _line)
    {
        foreach (RoomStruct room in _line)
        {
            if(min_y > room.y)
            {
                min_y = room.y;
            }
            if (max_y < room.y)
            {
                max_y = room.y;
            }
            if (min_x > room.x)
            {
                min_x = room.x;
            }
            if (max_x < room.x)
            {
                max_x = room.x;
            }
        }
    }
}
