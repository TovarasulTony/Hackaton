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
            switch(assignedRow)
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
                Debug.Log(i + " " + j);
                Debug.Log(m_MapMatrix.Length);
                m_MapMatrix[i, j] = STRUCTURE_TYPE.Tile;
            }
        }
    }

    void MakePath(RoomStruct _first, RoomStruct _second)
    {
        int direction = _first.y - _second.y > 0 ? -1 : 1;
        //Debug.Log("secodn number " + _second.number);
        //Debug.Log("shop x "+_first.x);
        //Debug.Log("shop y " + _first.y);
        for (int i = _first.y; i != _second.y + direction; i = i + direction)
        {
            //Debug.Log(i);
            m_MapMatrix[_first.x, i] = STRUCTURE_TYPE.Tile;
        }
        direction = _first.x - _second.x > 0 ? -1 : 1;
        for (int i = _first.x; i != _second.x + direction; i = i + direction)
        {
            //Debug.Log(i);
            m_MapMatrix[i, _second.y] = STRUCTURE_TYPE.Tile;
        }
    }

    public List<RoomStruct> GenerateRoomsss(STRUCTURE_TYPE[,] _mapMatrix, int _matrixLength)
    {
        int roomsNumber = Random.Range(5, 6);
        m_RoomsList = new List<RoomStruct>();
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
                room_X = Random.Range(5, _matrixLength - 5);
                room_Y = Random.Range(5, _matrixLength - 5);

                roomOk = true;
                for (int i = room_X - (roomHeigth - 1) / 2 - 1; i <= room_X + roomHeigth / 2 + 1; ++i)
                {
                    for (int j = room_Y - (roomHeigth - 1) / 2 - 1; j <= room_Y + roomHeigth / 2 + 1; ++j)
                    {
                        if (_mapMatrix[i, j] != STRUCTURE_TYPE.Wall)
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
                RoomStruct room = new RoomStruct();
                room.x = room_X;
                room.y = room_Y;
                room.width = roomWidth;
                room.heigth = roomHeigth;
                m_RoomsList.Add(room);
                for (int i = room_X - (roomHeigth - 1) / 2; i <= room_X + roomHeigth / 2; ++i)
                {
                    for (int j = room_Y - (roomWidth - 1) / 2; j <= room_Y + roomWidth / 2; ++j)
                    {
                        _mapMatrix[i, j] = STRUCTURE_TYPE.Tile;
                    }
                }
            }
        }

        return m_RoomsList;
    }

    public Dictionary<RoomStruct,RoomStruct> Paths()
    {
        m_MinRoom = new Dictionary<RoomStruct, RoomStruct>();
        /*


        foreach (RoomStruct room in m_RoomsList)
        {
            RoomStruct otherRoom = FindMinPath(room);
            if(m_MinRoom.ContainsKey(otherRoom))
            {
                if( m_MinRoom[otherRoom] == room)
                {
                    continue;
                }
            }
            m_MinRoom.Add(room, FindMinPath(room));
        }
        */
        return m_MinRoom;
    }

    RoomStruct FindMinPath(RoomStruct _room)
    {
        int minDistance = 10000;
        RoomStruct minRoom = null;
        foreach (RoomStruct room in m_RoomsList)
        {
            int distance = System.Math.Abs(room.x - _room.x) + System.Math.Abs(room.y - _room.y);
            if (minDistance > distance)
            {
                minRoom = room;
                minDistance = distance;
            }
        }
        return minRoom;
    }
}
