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
        DropGold();
        DestroyThis();
    }

    void DropGold()
    {
        Gold gold = Instantiate(Library.instance.GetPrefab("Gold")).GetComponent<Gold>();
        gold.SetGold(Random.Range(1, 101));
        m_CurrentTile.AddToTile(gold);
        float x = gold.GetTileReference().transform.position.x;
        float y = gold.GetTileReference().transform.position.y;
        float z = gold.GetTileReference().GetLayerNumber();
        gold.transform.position = new Vector3(x, y, z);
    }
}
