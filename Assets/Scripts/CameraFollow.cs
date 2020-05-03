using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    Invalid,
    None,
    Up,
    Down,
    Left,
    Right
}

public class CameraFollow : MonoBehaviour, IPlayerSubscriber
{
    public Player m_Player;
    Vector3 m_TargetPosition;
    DIRECTION m_Direction = DIRECTION.None;

    const float c_PositionZ = -20;

    void Start()
    {
        m_Player.Subscribe(GetComponent<IPlayerSubscriber>());
        m_TargetPosition = m_Player.GetTileReference().transform.position;

        transform.position = new Vector3(m_TargetPosition.x, m_TargetPosition.y, c_PositionZ);
    }


    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if(m_Direction == DIRECTION.None)
        {
            return;
        }
        float cameraSpeed = Time.deltaTime * 5;
        switch(m_Direction)
        {
            case DIRECTION.Up:
                transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
                if(transform.position.y >= m_TargetPosition.y)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                }
                break;
            case DIRECTION.Down:
                transform.position = new Vector3(transform.position.x, transform.position.y - cameraSpeed, transform.position.z);
                if (transform.position.y <= m_TargetPosition.y)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                }
                break;
            case DIRECTION.Right:
                transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
                if (transform.position.x >= m_TargetPosition.x)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                }
                break;
            case DIRECTION.Left:
                transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
                if (transform.position.x <= m_TargetPosition.x)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                }
                break;
        }

    }

    public void OnPlayerMovement()
    {
        m_TargetPosition = m_Player.GetTileReference().transform.position;
        m_TargetPosition = new Vector3(m_TargetPosition.x, m_TargetPosition.y, transform.position.z);
        if (m_TargetPosition.x > transform.position.x)
        {
            m_Direction = DIRECTION.Right;
        }
        if (m_TargetPosition.x < transform.position.x)
        {
            m_Direction = DIRECTION.Left;
        }
        if (m_TargetPosition.y > transform.position.y)
        {
            m_Direction = DIRECTION.Up;
        }
        if (m_TargetPosition.y < transform.position.y)
        {
            m_Direction = DIRECTION.Down;
        }
    }

    public void SetPlayerReference(Player _player)
    {
        m_Player = _player;
    }
}
