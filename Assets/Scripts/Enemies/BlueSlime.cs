using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BLUE_SLIME_STATE
{
    Invalid,
    Up,
    PauseOne,
    Down,
    PauseTwo
}

public class BlueSlime : Enemy, IBeat
{
    BLUE_SLIME_STATE m_State;

    protected override void StartEnemy()
    {
        m_State = BLUE_SLIME_STATE.Up;
        BeatMaster.instance.SubscribeToBeat(GetComponent<IBeat>());
    }
    
    public void OnBeat()
    {
        switch (m_State)
        {
            case BLUE_SLIME_STATE.Up:
                Moving(DIRECTION.Up);
                break;
            case BLUE_SLIME_STATE.Down:
                Moving(DIRECTION.Down);
                break;
        }
        NextState();
    }

    void Moving(DIRECTION _direction)
    {
        Tile nextTile = m_CurrentTile.GetTile(_direction);
        if(nextTile.GetNumberOfElements() != 0)
        {
            return;
        }
        m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
        m_CurrentTile = nextTile;
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        transform.position = new Vector3(m_CurrentTile.transform.position.x, m_CurrentTile.transform.position.y, transform.position.z);
    }

    void NextState()
    {
        switch(m_State)
        {
            case BLUE_SLIME_STATE.Up:
                m_State = BLUE_SLIME_STATE.PauseOne;
                break;
            case BLUE_SLIME_STATE.PauseOne:
                m_State = BLUE_SLIME_STATE.Down;
                break;
            case BLUE_SLIME_STATE.Down:
                m_State = BLUE_SLIME_STATE.PauseTwo;
                break;
            case BLUE_SLIME_STATE.PauseTwo:
                m_State = BLUE_SLIME_STATE.Up;
                break;
            default:
                m_State = BLUE_SLIME_STATE.Invalid;
                break;
        }
    }
}
