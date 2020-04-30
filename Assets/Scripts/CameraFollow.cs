using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IPlayerSubscriber
{
    public Player m_Player;
    Vector3 m_TargetPosition;
    bool m_PositionReached = true;

    const float c_PositionZ = -20;

    void Start()
    {
        Debug.Log(GetComponent<IPlayerSubscriber>());
        Debug.Log(m_Player);
        m_Player.Subscribe(GetComponent<IPlayerSubscriber>());
        m_TargetPosition = m_Player.GetTileReference().transform.position;

        transform.position = new Vector3(m_TargetPosition.x, m_TargetPosition.y, c_PositionZ);
        Debug.Log(m_TargetPosition);
        Debug.Log("Camera");
    }


    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if(m_PositionReached == true)
        {
            return;
        }


    }

    public void OnPlayerMovement()
    {
        m_PositionReached = false;
        m_TargetPosition = m_Player.GetTileReference().transform.position;
    }

    public void SetPlayerReference(Player _player)
    {
        m_Player = _player;
    }
}
