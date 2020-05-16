using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder: IPlayerSubscriber
{
    Player m_Player;
    int[,] m_DistanceMatrix;
    bool[,] m_UpdatedTiles;
    int m_MatrixLength = 30;

    public Pathfinder()
    {
        m_MatrixLength = Map.instance.GetMatrixLength();
        m_DistanceMatrix = new int[m_MatrixLength, m_MatrixLength];
        m_UpdatedTiles = new bool[m_MatrixLength, m_MatrixLength];

        for (int i = 0; i < m_MatrixLength; ++i)
        {
            for (int j = 0; j < m_MatrixLength; ++j)
            {
                m_DistanceMatrix[i, j] = -1;
                m_UpdatedTiles[i, j] = false;
            }
        }
    }

    public void OnPlayerMovement(DIRECTION _direction)
    {
        Clean();
        UpdateDistanceMatrix();
    }

    void Clean()
    {
        for (int i = 0; i < m_MatrixLength; ++i)
        {
            for (int j = 0; j < m_MatrixLength; ++j)
            {
                m_DistanceMatrix[i, j] = -1;
                m_UpdatedTiles[i, j] = false;
            }
        }
    }

    void UpdateDistanceMatrix()
    {
        STRUCTURE_TYPE[,] structureMap = Map.instance.GetStructureMap();
        Queue<KeyValuePair<Tile, int>> tileQueue = new Queue<KeyValuePair<Tile, int>>();
        tileQueue.Enqueue(new KeyValuePair<Tile, int>(m_Player.GetTileReference(), 0));

        while(tileQueue.Count > 0)
        {
            KeyValuePair<Tile, int> pair = tileQueue.Dequeue();
            int x = pair.Key.GetX();
            int y = pair.Key.GetY();
            m_DistanceMatrix[x, y] = pair.Value;
            m_UpdatedTiles[x, y] = true;
            EnqueueTile(pair.Key.GetTile(DIRECTION.Up), pair.Value + 1, ref tileQueue);
            EnqueueTile(pair.Key.GetTile(DIRECTION.Down), pair.Value + 1, ref tileQueue);
            EnqueueTile(pair.Key.GetTile(DIRECTION.Left), pair.Value + 1, ref tileQueue);
            EnqueueTile(pair.Key.GetTile(DIRECTION.Right), pair.Value + 1, ref tileQueue);
        }
    }

    void EnqueueTile(Tile _tile, int _distance, ref Queue<KeyValuePair<Tile, int>> _tileQueue)
    {
        if (_tile.Contains<Wall>() != null)
        {
            return;
        }
        if (m_UpdatedTiles[_tile.GetX(), _tile.GetY()] == true)
        {
            return;
        }
        _tileQueue.Enqueue(new KeyValuePair<Tile, int>(_tile, _distance));
    }

    public void SetPlayerReference(Player _player)
    {
        m_Player = _player;
        m_Player.Subscribe(this);
    }

    public int GetPlayerDistance(Tile _tile)
    {
        return m_DistanceMatrix[_tile.GetX(), _tile.GetY()];
    }
}
