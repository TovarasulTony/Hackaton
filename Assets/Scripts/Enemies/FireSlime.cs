using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum FIRE_SLIME_STATE
{
    Invalid,
    Up,
    Left,
    Down,
    Right
}

public class FireSlime : Enemy
{
    FIRE_SLIME_STATE m_State;

    protected override void StartEnemy()
    {
        m_State = FIRE_SLIME_STATE.Up;
        BeatMaster.instance.SubscribeToBeat(GetComponent<IBeat>());
    }

    public override void OnBeat()
    {
        switch (m_State)
        {
            case FIRE_SLIME_STATE.Up:
                Move(DIRECTION.Down,DIRECTION.Left);
                break;
            case FIRE_SLIME_STATE.Left:
                Move(DIRECTION.Right,DIRECTION.Down);
                break;
            case FIRE_SLIME_STATE.Down:
                Move(DIRECTION.Up,DIRECTION.Right);
                break;
            case FIRE_SLIME_STATE.Right:
                Move(DIRECTION.Left,DIRECTION.Up);
                break;
        }
        NextState();
    }

    void Move(DIRECTION _first_direction, DIRECTION _final_direction)
    {
        Tile nextTile = m_CurrentTile.GetTile(_first_direction);
        Tile finalTile = nextTile.GetTile(_final_direction);
        if (finalTile.Contains<Wall>() != null)
        {
            finalTile.Contains<Wall>().GetComponent<Wall>().Dig(1);
        }

        m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
        m_CurrentTile = finalTile;
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        transform.position = new Vector3(m_CurrentTile.transform.position.x, m_CurrentTile.transform.position.y, transform.position.z);

    }

    void NextState()
    {
        switch (m_State)
        {
            case FIRE_SLIME_STATE.Up:
                m_State = FIRE_SLIME_STATE.Left;
                break;
            case FIRE_SLIME_STATE.Left:
                m_State = FIRE_SLIME_STATE.Down;
                break;
            case FIRE_SLIME_STATE.Down:
                m_State = FIRE_SLIME_STATE.Right;
                break;
            case FIRE_SLIME_STATE.Right:
                m_State = FIRE_SLIME_STATE.Up;
                break;
            default:
                m_State = FIRE_SLIME_STATE.Invalid;
                break;
        }
    }
}
