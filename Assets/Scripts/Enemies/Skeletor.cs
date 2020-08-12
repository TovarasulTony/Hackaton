using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKELETOR_STATE
{
    Invalid,
    Move,
    Pause
}

public class Skeletor : Enemy
{
    SKELETOR_STATE m_State;
    DIRECTION m_MovementDirection;
    Vector3 m_NewPosition;

    protected override void StartEnemy()
    {
        m_State = SKELETOR_STATE.Pause;

        SkeletorAnimation skeletorAnimation = new SkeletorAnimation();
        skeletorAnimation.SetSkeletorReference(GetComponent<Skeletor>());
        mBehaviorsList.Add(skeletorAnimation);
    }

    public override void OnBeat()
    {
        if(m_State == SKELETOR_STATE.Move)
        {
            Move();
        }
        NextState();
    }

    void Move()
    {
        int minDistance = 100;
        Tile nextTile = null;
        int distance;

        foreach (DIRECTION direction in new DIRECTION[] { DIRECTION.Up, DIRECTION.Down, DIRECTION.Left, DIRECTION.Right })
        {
            distance = Map.instance.GetPlayerDistance(m_CurrentTile.GetTile(direction));
            if (minDistance > distance && distance != -1)
            {
                minDistance = distance;
                nextTile = m_CurrentTile.GetTile(direction);
                m_MovementDirection = direction;
            }
        }
       
        if(nextTile == null)
        {
            return;
        }
        /*
        if (nextTile.GetNumberOfElements() != 0)
        {
            return;
        }*/
        m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
        m_CurrentTile = nextTile;
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        m_NewPosition = new Vector3(m_CurrentTile.transform.position.x, m_CurrentTile.transform.position.y, m_CurrentTile.GetLayerNumber());
    }

    void NextState()
    {
        switch (m_State)
        {
            case SKELETOR_STATE.Move:
                m_State = SKELETOR_STATE.Pause;
                break;
            case SKELETOR_STATE.Pause:
                m_State = SKELETOR_STATE.Move;
                break;
            default:
                m_State = SKELETOR_STATE.Invalid;
                break;
        }
    }

    public SKELETOR_STATE GetState()
    {
        return m_State;
    }

    public Vector3 GetNewPosition()
    {
        return m_NewPosition;
    }

    public DIRECTION GetDirection()
    {
        return m_MovementDirection;
    }
}
