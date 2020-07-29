using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    //SortedList<int, RoomStruct> m_RoomsList;
    List<RoomStruct> m_RoomsList;
    int m_MatrixLength;
    STRUCTURE_TYPE[,] m_MapMatrix;

    int m_ShopX;
    int m_ShopY;

    //optimisation much?
    //int[,] m_ReducedMatrix;

    public MapGenerator()
    {
        m_MatrixLength = Map.instance.GetMatrixLength();
        m_MapMatrix = new STRUCTURE_TYPE[m_MatrixLength, m_MatrixLength];
        m_RoomsList = new List<RoomStruct>();

        SetupMatix();
        SetupShop();
        SetupRooms();
        SetupPaths();

        Map.instance.SetStructureMap(m_MapMatrix);
    }

    void SetupShop()
    {/*
        int min_range = 6;
        int max_range = m_MatrixLength - 6;

        int shop_X = Random.Range(min_range + 2, max_range + 2);
        int shop_Y = Random.Range(min_range, max_range);

        m_ShopX = shop_X;
        m_ShopY = shop_Y;
        RoomStruct shop = new RoomStruct();
        shop.x = m_ShopX;
        shop.y = m_ShopY;
        shop.number = 0;
        m_RoomsList.Add(shop);
        Map.instance.AddCoordinates("Shop", new KeyValuePair<int, int>(m_ShopX, m_ShopY));


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
        m_MapMatrix[shop_X - 4, shop_Y] = STRUCTURE_TYPE.Tile;
        m_MapMatrix[shop_X, shop_Y] = STRUCTURE_TYPE.Shop;
        */
    }

    void SetupMatix()
    {
        for (int i = 0; i < m_MatrixLength; ++i)
        {
            for (int j = 0; j < m_MatrixLength; ++j)
            {
                m_MapMatrix[i, j] = STRUCTURE_TYPE.Wall;
            }
        }
    }

    void SetupRooms()
    {
        RoomGenerator roomGenerator = new RoomGenerator(ref m_MapMatrix, m_MatrixLength);
        //m_RoomsList = roomGenerator.GenerateRooms();
        roomGenerator.GenerateRooms();
        /*
        Dictionary<RoomStruct, RoomStruct> ceva = roomGenerator.Paths();

        foreach (KeyValuePair<RoomStruct, RoomStruct> room in ceva)
        {
            //MakePath(room.Key, room.Value);
            /*if(ceva[i] == 0)
            {
                continue;
            }
            Debug.Log(i + " " + ceva[i]);
            MakePath(finishedRooms[currentRoom], finishedRooms[minRoom]);
        }*/
    }

    void SetupPaths()
    {
        /*
        List<RoomStruct> finishedRooms = new List<RoomStruct>();
        m_RoomsList[0].x -= 5;
        int minDistance = 1000;
        int minRoom = 0;
        int ro = 0;
        foreach(RoomStruct room in m_RoomsList)
        {
            room.number = ro;
            ++ro;
            if (room.number != m_RoomsList[0].number)
            {
                int distance = System.Math.Abs(room.x - m_RoomsList[0].x) + System.Math.Abs(room.y - m_RoomsList[0].y);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minRoom = room.number;
                }
            }
        }
        finishedRooms.Add(m_RoomsList[0]);
        finishedRooms.Add(m_RoomsList[minRoom]);
        m_RoomsList.Remove(m_RoomsList[minRoom]);
        m_RoomsList.Remove(m_RoomsList[0]);
        MakePath(finishedRooms[0], finishedRooms[1]);
        int currentRoom = 1;
        while (m_RoomsList.Count != 0)
        {
            minDistance = 1000;
            ro = 0;
            foreach (RoomStruct room in m_RoomsList)
            {
                room.number = 0;
                ro++;
                int distance = System.Math.Abs(room.x - finishedRooms[currentRoom].x) + System.Math.Abs(room.y - finishedRooms[currentRoom].y);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minRoom = room.number;
                }
            }
            finishedRooms.Add(m_RoomsList[minRoom]);
            m_RoomsList.Remove(m_RoomsList[minRoom]);
            MakePath(finishedRooms[currentRoom], finishedRooms[minRoom]);
            currentRoom++;
        }
        */
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
}
