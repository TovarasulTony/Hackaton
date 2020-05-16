using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AboveTileObject
{
    TileGenerator m_TileGenerator;

    public void SetTileGenerator(TileGenerator _generatorReference)
    {
        m_TileGenerator = _generatorReference;
    }

    protected override void StartActor()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = m_TileGenerator.GetEnemyTile();
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        SetFogStatus(FOG_STATUS.Unexplored);
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);

        StartEnemy();
    }

    protected virtual void StartEnemy() { }
}
