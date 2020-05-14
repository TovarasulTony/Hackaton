using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy m_Enemy;

    void Start()
    {
        Enemy enemy = Instantiate(m_Enemy);
    }
}
