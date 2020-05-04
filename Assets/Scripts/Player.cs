using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MOVEMENT_STATUS
{
    Invalid,
    Standing,
    MovingCombo,
    MovingAnimation,
    Timeout
}

public enum DIRECTION
{
    Invalid,
    None,
    Up,
    Down,
    Left,
    Right
}

public class Player : AboveTileObject
{
    private TileGenerator m_TileGenerator;
    List<IPlayerSubscriber> m_SubscriberList;
    //bool m_IsMoving = false;
    //bool m_Timeout = false;
    MOVEMENT_STATUS m_MovementStatus = MOVEMENT_STATUS.Standing;
    DIRECTION m_MovementDirection = DIRECTION.None;
    DIRECTION m_Facing = DIRECTION.Right;
    float m_TimeoutTime = 0;
    int m_MovementBeat;
    Vector3 m_NewPosition;

    //shitty practice
    public Transform m_AnimationTransform;
    float m_OldY;


    //Bad Practice
    public MissedBeat m_MissedBeatPrefab;

    void Start()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = m_TileGenerator.GetStartingTile();
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);
    }

    private void FixedUpdate()
    {
        HandleTimeout();
        MovingAnimation();
    }

    void Update()
    {
        HandleInput();
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
            return;
        }
        m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
        m_CurrentTile = _nextTile;
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        Vector3 new_position = m_CurrentTile.transform.position;
        m_NewPosition = new Vector3(new_position.x, new_position.y, -2f);
        m_MovementStatus = MOVEMENT_STATUS.MovingAnimation;
        m_OldY = m_AnimationTransform.position.y;

        if (m_NewPosition.x > transform.position.x)
        {
            m_MovementDirection = DIRECTION.Right;
            if (m_Facing == DIRECTION.Left)
            {
                m_Facing = DIRECTION.Right;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (m_NewPosition.x < transform.position.x)
        {
            m_MovementDirection = DIRECTION.Left;
            if(m_Facing == DIRECTION.Right)
            {
                m_Facing = DIRECTION.Left;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (m_NewPosition.y > transform.position.y)
        {
            m_MovementDirection = DIRECTION.Up;
        }
        if (m_NewPosition.y < transform.position.y)
        {
            m_MovementDirection = DIRECTION.Down;
        }

        NotifySubscribers();
    }

    bool CheckCanMove()
    {
        if(m_MovementStatus == MOVEMENT_STATUS.MovingAnimation)
        {
            return false;
        }
        if (m_MovementStatus == MOVEMENT_STATUS.Timeout)
        {
            MissedBeat();
            return false;
        }
        if (m_MovementStatus == MOVEMENT_STATUS.Standing)
        {
            m_MovementStatus = MOVEMENT_STATUS.MovingCombo;
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
        m_MovementStatus = MOVEMENT_STATUS.Timeout;
        m_TimeoutTime = 0;

        //fuck... chestia asta e urata rau de tot;
        //pe viitor trebuie sa am pe cineva care se ocupa de UI
        //pe asta ofac aici asa rapid pt cv vizual; BAD PRACTICE
        Instantiate(m_MissedBeatPrefab);
    }

    void HandleTimeout()
    {
        if (m_MovementStatus != MOVEMENT_STATUS.Timeout)
        {
            return;
        }
        m_TimeoutTime += Time.fixedDeltaTime;
        if(m_TimeoutTime >= 0.5f)
        {
            m_MovementStatus = MOVEMENT_STATUS.Standing;
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
        
        foreach (IPlayerSubscriber subscriber in m_SubscriberList)
        {
            subscriber.OnPlayerMovement(m_MovementDirection);
        }
    }

    void MovingAnimation()
    {
        if(m_MovementStatus != MOVEMENT_STATUS.MovingAnimation)
        {
            return;
        }
        float speed = Time.fixedDeltaTime * 7;
        switch (m_MovementDirection)
        {
            case DIRECTION.Up:
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_AnimationTransform.position.y + speed / 3, m_AnimationTransform.position.z);
                if (transform.position.y >= m_NewPosition.y)
                {
                    DestinationReached();
                    m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_OldY + 1, m_AnimationTransform.position.z);
                }
                break;
            case DIRECTION.Down:
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_AnimationTransform.position.y + speed / 3, m_AnimationTransform.position.z);
                if (transform.position.y <= m_NewPosition.y)
                {
                    DestinationReached();
                    m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_OldY - 1, m_AnimationTransform.position.z);
                }
                break;
            case DIRECTION.Right:
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_AnimationTransform.position.y + speed/2, m_AnimationTransform.position.z);
                if (transform.position.x >= m_NewPosition.x)
                {
                    DestinationReached();
                    m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_OldY, m_AnimationTransform.position.z);
                }
                break;
            case DIRECTION.Left:
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_AnimationTransform.position.y + speed/2, m_AnimationTransform.position.z);
                if (transform.position.x <= m_NewPosition.x)
                {
                    DestinationReached();
                    m_AnimationTransform.position = new Vector3(m_AnimationTransform.position.x, m_OldY, m_AnimationTransform.position.z);
                }
                break;
        }
    }

    void DestinationReached()
    {
        m_MovementDirection = DIRECTION.None;
        transform.position = m_NewPosition;
        m_MovementStatus = MOVEMENT_STATUS.MovingCombo;
    }

    public void SetTileGenerator(TileGenerator _generatorReference)
    {
        m_TileGenerator = _generatorReference;
    }
}

