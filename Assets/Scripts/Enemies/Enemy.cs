using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AboveTileObject, IBeat
{
    protected override void StartActor()
    {
        Map.instance.AddEnemy(GetComponent<Enemy>());
        BeatMaster.instance.SubscribeToBeat(GetComponent<IBeat>());
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, m_CurrentTile.GetLayerNumber());

        StartEnemy();
    }

    protected virtual void StartEnemy() { }

    public virtual void OnBeat() { }

    public void DestroyEnemy() 
    {
        BeatMaster.instance.UnsubscribeToBeat(GetComponent<IBeat>());
        Map.instance.RemoveEnemy(GetComponent<Enemy>());
        DestroyThis();
    }
}
