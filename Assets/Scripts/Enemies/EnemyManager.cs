using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy m_Enemy;
    TileGenerator m_TileGenerator;


    public void SetTileGenerator(TileGenerator _generatorReference)
    {
        m_TileGenerator = _generatorReference;
    }

    void Start()
    {
        Enemy enemy = Instantiate(m_Enemy);
        enemy.SetTileGenerator(m_TileGenerator);
    }
}
