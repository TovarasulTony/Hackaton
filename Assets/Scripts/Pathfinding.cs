using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding instance = null;
    int[,] m_DistanceMatrix;
    int m_MatrixLength = 30;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("BeatMaster instantiat de doua ori");
            Destroy(gameObject);
        }
        instance = gameObject.GetComponent<Pathfinding>();
        m_DistanceMatrix = new int[m_MatrixLength, m_MatrixLength];

        for (int i = 0; i < m_MatrixLength; ++i)
        {
            for (int j = 0; j < m_MatrixLength; ++j)
            {
                m_DistanceMatrix[i, j] = -1;
            }
        }
    }
}
