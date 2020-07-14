using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Schimba codul de move . 2 argumente pentru cele 2 directii in loc de 2 apeluri.
 Vezi cum faci sa sapi in caz ca trebuie sa sari in perete de pamant
 
 */


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
                Move(DIRECTION.Down);
                Move(DIRECTION.Left,true);
                break;
            case FIRE_SLIME_STATE.Left:
                Move(DIRECTION.Right);
                Move(DIRECTION.Down,true);
                break;
            case FIRE_SLIME_STATE.Down:
                Move(DIRECTION.Up);
                Move(DIRECTION.Right,true);
                break;
            case FIRE_SLIME_STATE.Right:
                Move(DIRECTION.Left);
                Move(DIRECTION.Up,true);
                break;
        }
        NextState();
    }

    void Move(DIRECTION _direction, bool move_character=false)
    {
        
        Tile nextTile = m_CurrentTile.GetTile(_direction);
        if (nextTile.GetNumberOfElements() != 0)
        {
            return;
        }
        
        if (move_character)
        {
            m_CurrentTile = nextTile;
            m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
            transform.position = new Vector3(m_CurrentTile.transform.position.x, m_CurrentTile.transform.position.y, transform.position.z);
        }
        else
        {
            m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
            m_CurrentTile = nextTile;
        }
            
        


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
