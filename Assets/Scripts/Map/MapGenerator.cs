using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    int m_MatrixLength;
    STRUCTURE_TYPE[,] m_MapMatrix;
    Map m_MapReference;

    int m_ShopX;
    int m_ShopY;

    //optimisation much?
    //int[,] m_ReducedMatrix;

    public MapGenerator(Map _reference)
    {
        m_MapReference = _reference;
        m_MatrixLength = m_MapReference.GetMatrixLength();
        m_MapMatrix = new STRUCTURE_TYPE[m_MatrixLength, m_MatrixLength];

        SetupMatix();
        SetupShop();
        SetupRooms();

        m_MapReference.SetStructureMap(m_MapMatrix);
    }

    void SetupShop()
    {
        int min_range = 6;
        int max_range = m_MatrixLength - 6;

        int shop_X = Random.Range(min_range + 2, max_range + 2);
        int shop_Y = Random.Range(min_range, max_range);

        m_ShopX = shop_X;
        m_ShopY = shop_Y;
        m_MapReference.AddCoordinates("Shop", new KeyValuePair<int, int>(m_ShopX, m_ShopY));


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
}
