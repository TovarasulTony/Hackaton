﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKELETOR_STATE
{
    Invalid,
    Move,
    Pause
}

public class Skeletor : Enemy, IBeat
{
    SKELETOR_STATE m_State;
    DIRECTION m_MovementDirection;
    Vector3 m_NewPosition;

    protected override void StartEnemy()
    {
        m_State = SKELETOR_STATE.Pause;
        BeatMaster.instance.SubscribeToBeat(GetComponent<IBeat>());

        SkeletorAnimation skeletorAnimation = new SkeletorAnimation();
        skeletorAnimation.SetSkeletorReference(GetComponent<Skeletor>());
        mBehaviorsList.Add(skeletorAnimation);
    }

    public void OnBeat()
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
        int distance = Map.instance.GetPlayerDistance(m_CurrentTile.GetTile(DIRECTION.Up));
       // Debug.Log(distance);
        if (minDistance > distance && distance != -1)
        {
            minDistance = distance;
            nextTile = m_CurrentTile.GetTile(DIRECTION.Up);
            m_MovementDirection = DIRECTION.Up;
        }
        distance = Map.instance.GetPlayerDistance(m_CurrentTile.GetTile(DIRECTION.Down));
       // Debug.Log(distance);
        if (minDistance > distance && distance != -1)
        {
            minDistance = distance;
            nextTile = m_CurrentTile.GetTile(DIRECTION.Down);
            m_MovementDirection = DIRECTION.Down;
        }
        distance = Map.instance.GetPlayerDistance(m_CurrentTile.GetTile(DIRECTION.Left));
       // Debug.Log(distance);
        if (minDistance > distance && distance != -1)
        {
            minDistance = distance;
            nextTile = m_CurrentTile.GetTile(DIRECTION.Left);
            m_MovementDirection = DIRECTION.Left;
        }
        distance = Map.instance.GetPlayerDistance(m_CurrentTile.GetTile(DIRECTION.Right));
       // Debug.Log(distance);
        if (minDistance > distance && distance != -1)
        {
            minDistance = distance;
            nextTile = m_CurrentTile.GetTile(DIRECTION.Right);
            m_MovementDirection = DIRECTION.Right;
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
        m_NewPosition = new Vector3(m_CurrentTile.transform.position.x, m_CurrentTile.transform.position.y, transform.position.z);
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
