using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FOG_STATUS
{
    Invalid,
    Unexplored,
    Explored,
    InVision
}

public class FogOfWar : GenericBehavior, IPlayerSubscriber
{
    List<KeyValuePair<int, int>> m_LightCoordinatesList;
    Player m_PlayerReference;

    public FogOfWar(Player _reference)
    {
        m_PlayerReference = _reference;
        m_PlayerReference.Subscribe(this);
    }

    protected override void StartMyBehavior()
    {
        m_LightCoordinatesList = new List<KeyValuePair<int, int>>();
        for (int i = -2; i <= 2; ++i)
        {
            for (int j = -2; j <= 2; ++j)
            {
                m_LightCoordinatesList.Add(new KeyValuePair<int, int>(i, j));
            }
        }
        Explore();
    }

    public void OnPlayerMovement(DIRECTION _direction)
    {
        RefreshArea();
        Explore();
    }

    void Explore()
    {
        foreach(KeyValuePair<int, int> pair in m_LightCoordinatesList)
        {
            //poate trebuie schimbata locatia functiei
            Tile tile = m_PlayerReference.GetTileFromPair(pair, m_PlayerReference.GetTileReference());
            tile.SetFogStatus(FOG_STATUS.InVision);
        }
    }

    void RefreshArea()
    {
        foreach (KeyValuePair<int, int> pair in m_LightCoordinatesList)
        {
            //poate trebuie schimbata locatia functiei
            Tile tile = m_PlayerReference.GetTileFromPair(pair, m_PlayerReference.GetOldTile());
            tile.SetFogStatus(FOG_STATUS.Explored);
        }
    }
}
