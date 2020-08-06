using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IPlayerSubscriber
{
    public Player m_Player;
    Vector3 m_TargetPosition;
    DIRECTION m_Direction = DIRECTION.None;
    //valoare harcodata, ar trebui in functie de beat dar imi e lene
    float m_ShakeTime = 0.2f;
    float m_PassedTime = 0;
    bool m_IsShaking = false;

    float debugTime = 0;

    const float c_PositionZ = -20;

    void Start()
    {
        m_Player.Subscribe(GetComponent<IPlayerSubscriber>());
        m_TargetPosition = m_Player.GetTileReference().transform.position;

        transform.position = new Vector3(m_TargetPosition.x, m_TargetPosition.y, c_PositionZ);
        m_TargetPosition = transform.position;
    }

    void FixedUpdate()
    {
        debugTime += Time.fixedDeltaTime;
        FollowPlayer();
        if(m_IsShaking)
        {
            transform.position = new Vector3(m_TargetPosition.x + Random.Range(-100, 100) * 0.001f, m_TargetPosition.y + Random.Range(-100, 100) * 0.001f, m_TargetPosition.z);
            m_PassedTime += Time.fixedDeltaTime;
        }
        if(m_PassedTime >= m_ShakeTime)
        {
            m_PassedTime = 0;
            transform.position = m_TargetPosition;
            m_IsShaking = false;
        }
    }

    public void CameraShake()
    {
        m_IsShaking = true;
    }

    void FollowPlayer()
    {
        if(m_Direction == DIRECTION.None)
        {
            return;
        }
        float cameraSpeed = Time.deltaTime * 7;
        switch(m_Direction)
        {
            case DIRECTION.Up:
                transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
                if(transform.position.y >= m_TargetPosition.y)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                    debugTime = 0;
                }
                break;
            case DIRECTION.Down:
                transform.position = new Vector3(transform.position.x, transform.position.y - cameraSpeed, transform.position.z);
                if (transform.position.y <= m_TargetPosition.y)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                    debugTime = 0;
                }
                break;
            case DIRECTION.Right:
                transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
                if (transform.position.x >= m_TargetPosition.x)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                    debugTime = 0;
                }
                break;
            case DIRECTION.Left:
                transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
                if (transform.position.x <= m_TargetPosition.x)
                {
                    m_Direction = DIRECTION.None;
                    transform.position = m_TargetPosition;
                    debugTime = 0;
                }
                break;
        }

    }

    public void OnPlayerMovement(DIRECTION _direction)
    {
        debugTime = 0;

        m_TargetPosition = m_Player.GetTileReference().transform.position;
        m_TargetPosition = new Vector3(m_TargetPosition.x, m_TargetPosition.y, transform.position.z);
        m_Direction = _direction;
    }

    public void SetPlayerReference(Player _player)
    {
        m_Player = _player;
    }
}
