using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEMENT_STATUS
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
    Tile m_OldTile;
    List<IPlayerSubscriber> m_SubscriberList;
    //bool m_IsMoving = false;
    //bool m_Timeout = false;
    MOVEMENT_STATUS m_MovementStatus = MOVEMENT_STATUS.Standing;
    DIRECTION m_MovementDirection = DIRECTION.None;
    DIRECTION m_Facing = DIRECTION.Right;
    float m_TimeoutTime = 0;
    int m_MovementBeat;
    Vector3 m_NewPosition;

    //Work in progress
    Weapon m_Weapon = new Axe();

    //shitty practice
    public Transform m_AnimationTransform;


    //Bad Practice
    public MissedBeat m_MissedBeatPrefab;

    protected override void StartActor()
    {
        //m_SubscriberList = new List<IPlayerSubscriber>(); This is shitty; Trebuie un loader
        m_CurrentTile = Map.instance.GetTile("Starting");
        m_OldTile = m_CurrentTile;
        Vector3 new_position = m_CurrentTile.transform.position;
        transform.position = new Vector3(new_position.x, new_position.y, -2f);

        PlayerAnimation playerAnimation = new PlayerAnimation();
        playerAnimation.SetPlayerReference(GetComponent<Player>());
        InventoryManagement inventoryManagement = new InventoryManagement(GetComponent<Player>());

        mBehaviorsList.Add(playerAnimation);
        mBehaviorsList.Add(inventoryManagement);

        FogOfWar fogOfWar = new FogOfWar(GetComponent<Player>());
        //fogOfWar.SetPlayerReference(GetComponent<Player>());
        mBehaviorsList.Add(fogOfWar);
    }

    protected override void FixedUpdateActor()
    {
        HandleTimeout();
    }

    protected override void UpdateActor()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_MovementDirection = DIRECTION.Down;
            Moving(m_CurrentTile.GetTile(DIRECTION.Down));
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_MovementDirection = DIRECTION.Up;
            Moving(m_CurrentTile.GetTile(DIRECTION.Up));
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_MovementDirection = DIRECTION.Left;
            Moving(m_CurrentTile.GetTile(DIRECTION.Left));
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_MovementDirection = DIRECTION.Right;
            Moving(m_CurrentTile.GetTile(DIRECTION.Right));
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
        if (Attack() == true) 
        {
            return;
        }
        m_OldTile = m_CurrentTile;
        m_CurrentTile.RemoveFromTile(GetComponent<AboveTileObject>());
        m_CurrentTile = _nextTile;
        m_CurrentTile.AddToTile(GetComponent<AboveTileObject>());
        Vector3 new_position = m_CurrentTile.transform.position;
        m_NewPosition = new Vector3(new_position.x, new_position.y, -2f);
        m_MovementStatus = MOVEMENT_STATUS.MovingAnimation;
        UpdatePlayerDirection();

        NotifySubscribers();
    }

    void UpdatePlayerDirection()
    {
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
            if (m_Facing == DIRECTION.Right)
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
    }

    bool CheckCanMove()
    {
        if(m_MovementStatus == MOVEMENT_STATUS.MovingAnimation)
        {
            m_MovementDirection = DIRECTION.None;
            return false;
        }
        if (m_MovementStatus == MOVEMENT_STATUS.Timeout)
        {
            m_MovementDirection = DIRECTION.None;
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
            m_MovementDirection = DIRECTION.None;
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

    bool Attack()
    {
        bool attacked = false;
        List<Tile> tileList = new List<Tile>();
        foreach(KeyValuePair<int,int> pair in m_Weapon.GetAttackDictionary()[m_MovementDirection])
        {
            tileList.Add(GetTileFromPair(pair, m_CurrentTile));
        }

        foreach(Tile tile in tileList)
        {
            AboveTileObject enemy = tile.Contains<Enemy>();
            if(enemy != null)
            {
                attacked = true;
                enemy.GetComponent<Enemy>().DestroyEnemy();
            }
        }

        return attacked;
    }
    //hmm poate trebuie schimbata locatia acestei functii faine; poate nu pt ca tine de player idk must consult better
    public Tile GetTileFromPair(KeyValuePair<int, int> _pair, Tile _startingTile)
    {
        Tile returnTile = _startingTile;
        DIRECTION vertical = DIRECTION.Up;
        DIRECTION horizontal = DIRECTION.Right;
        int verticalInt = _pair.Key;
        int horizontalInt = _pair.Value;

        if (verticalInt < 0)
        {
            verticalInt *= -1;
            vertical = DIRECTION.Down;
        }
        if (horizontalInt < 0)
        {
            horizontalInt *= -1;
            horizontal = DIRECTION.Left;
        }

        for (int i = 1; i <= verticalInt; i++)
        {
            returnTile = returnTile.GetTile(vertical);
        }
        for (int i = 1; i <= horizontalInt; i++)
        {
            returnTile = returnTile.GetTile(horizontal);
        }

        return returnTile;
    }

    public Weapon Equip(Weapon _weapon)
    {
        Weapon weapon = m_Weapon;
        m_Weapon = _weapon;
        return weapon;
    }

    public Vector3 GetNewPosition()
    {
        return m_NewPosition;
    }

    public Transform GetAnimatorTransform()
    {
        return m_AnimationTransform;
    }

    public DIRECTION GetDirection()
    {
        return m_MovementDirection;
    }

    public void SetDirection(DIRECTION _direction)
    {
        m_MovementDirection = _direction;
    }

    public MOVEMENT_STATUS GetMovementStatus()
    {
        return m_MovementStatus;
    }

    public void SetMovementStatus(MOVEMENT_STATUS _status)
    {
        m_MovementStatus = _status;
    }

    public Tile GetOldTile()
    {
        return m_OldTile;
    }
}

