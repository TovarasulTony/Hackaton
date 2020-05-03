using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MOVEMENT_STATUS
{
    Invalid,
    Standing,
    Moving,
    Timeout,

}

public class Player : AboveTileObject
{
    private TileGenerator m_TileGenerator;
    List<IPlayerSubscriber> m_SubscriberList;
    //bool m_IsMoving = false;
    //bool m_Timeout = false;
    MOVEMENT_STATUS m_MovoementStatus = MOVEMENT_STATUS.Standing;
    float m_TimeoutTime = 0;
    int m_MovementBeat;


    //Bad Practice
    public MissedBeat m_MissedBeatPrefab;

    void Start()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = m_TileGenerator.GetStartingTile();
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);
    }

    void Update()
    {
        HandleInput();
        HandleTimeout();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Down));
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Up));
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Left));
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Moving(m_CurrentTile.GetTile(TILE_DIRECTION.Right));
        }
    }

    void Moving(Tile _nextTile)
    {
        if (CheckCanMove() == false)
        {
            return;
        }

        if (_nextTile.Contains<Wall>() != null)
        {
            _nextTile.Contains<Wall>().GetComponent<Wall>().Dig(1);
        }
        else
        {
            m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
            m_CurrentTile = _nextTile;
            m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
            Vector3 new_position = m_CurrentTile.transform.position;
            transform.position = new Vector3(new_position.x, new_position.y, -2f);
        }
        NotifySubscribers();
    }

    bool CheckCanMove()
    {
        if (m_MovoementStatus == MOVEMENT_STATUS.Timeout)
        {
            MissedBeat();
            return false;
        }
        if (m_MovoementStatus == MOVEMENT_STATUS.Standing)
        {
            m_MovoementStatus = MOVEMENT_STATUS.Moving;
            m_MovementBeat = BeatMaster.instance.GetBeatNumber();
            return true;
        }

        m_MovementBeat++;
        if (m_MovementBeat - BeatMaster.instance.GetBeatNumber() >= 1)
        {
            MissedBeat();
            return false;
        }



        return true;
    }

    void MissedBeat()
    {
        m_MovoementStatus = MOVEMENT_STATUS.Timeout;
        m_TimeoutTime = 0;

        //fuck... chestia asta e urata rau de tot;
        //pe viitor trebuie sa am pe cineva care se ocupa de UI
        //pe asta ofac aici asa rapid pt cv vizual; BAD PRACTICE
        Instantiate(m_MissedBeatPrefab);
    }

    void HandleTimeout()
    {
        if (m_MovoementStatus != MOVEMENT_STATUS.Timeout)
        {
            return;
        }
        m_TimeoutTime += Time.deltaTime;
        if(m_TimeoutTime >= 0.5f)
        {
            m_MovoementStatus = MOVEMENT_STATUS.Standing;
        }
    }



    public void Subscribe(IPlayerSubscriber _subscriber)
    {
        if(m_SubscriberList == null)
        {
            m_SubscriberList = new List<IPlayerSubscriber>();
        }
        m_SubscriberList.Add(_subscriber);
    }

    void NotifySubscribers()
    {
        if(m_SubscriberList == null)
        {
            return;
        }

        foreach(IPlayerSubscriber subscriber in m_SubscriberList)
        {
            subscriber.OnPlayerMovement();
        }
    }

    public void SetTileGenerator(TileGenerator _generatorReference)
    {
        m_TileGenerator = _generatorReference;
    }
}

