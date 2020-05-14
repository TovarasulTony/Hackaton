using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AboveTileObject
{
    protected override void StartActor()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = Map.instance.GetTile("Enemy");
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        SetFogStatus(FOG_STATUS.Unexplored);
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);
    }
}
