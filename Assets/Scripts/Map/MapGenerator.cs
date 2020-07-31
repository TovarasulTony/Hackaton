using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    RoomGenerator m_RoomGenrator;
    List<RoomStruct> m_RoomsList;
    int m_MatrixLength;
    STRUCTURE_TYPE[,] m_MapMatrix;

    public MapGenerator()
    {
        m_MatrixLength = Map.instance.GetMatrixLength();
        m_MapMatrix = new STRUCTURE_TYPE[m_MatrixLength, m_MatrixLength];
        m_RoomsList = new List<RoomStruct>();
        m_RoomGenrator = new RoomGenerator(ref m_MapMatrix, m_MatrixLength);

        SetupMatix();
        m_RoomGenrator.GenerateRooms();

        Map.instance.SetStructureMap(m_MapMatrix);
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
}
