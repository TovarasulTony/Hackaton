using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : GenericBehavior
{
    Player m_PlayerReference;
    MOVEMENT_STATUS m_MovementStatus;
    DIRECTION m_MovementDirection;
    Transform m_PlayerObject;
    Transform m_PlayerAnimatorObject;
    Vector3 m_NewPosition;
    bool m_MovementStarted = true;
    float m_OldY;

    public void SetPlayerReference(Player _reference)
    {
        m_PlayerReference = _reference;
        m_PlayerObject = m_PlayerReference.transform;
        m_PlayerAnimatorObject = m_PlayerReference.GetAnimatorTransform();
    }

    protected override void FixedUpdateMyBehavior()
    {
        m_MovementStatus = m_PlayerReference.GetMovementStatus();
        m_MovementDirection = m_PlayerReference.GetDirection();
        m_NewPosition = m_PlayerReference.GetNewPosition();
        MovingAnimation();
    }

    void MovingAnimation()
    {
        if (m_MovementStatus != MOVEMENT_STATUS.MovingAnimation)
        {
            return;
        }
        if (m_MovementStarted == true)
        {
            m_MovementStarted = false;
            m_OldY = m_PlayerAnimatorObject.position.y;
        }
        float speed = Time.fixedDeltaTime * 7;
        switch (m_MovementDirection)
        {
            case DIRECTION.Up:
                m_PlayerObject.position = new Vector3(m_PlayerObject.position.x, m_PlayerObject.position.y + speed, m_PlayerObject.position.z);
                m_PlayerAnimatorObject.position = new Vector3(m_PlayerAnimatorObject.position.x, m_PlayerAnimatorObject.position.y + speed / 3, m_PlayerAnimatorObject.position.z);
                if (m_PlayerObject.position.y >= m_NewPosition.y)
                {
                    m_OldY++;
                    DestinationReached();
                }
                break;
            case DIRECTION.Down:
                m_PlayerObject.position = new Vector3(m_PlayerObject.position.x, m_PlayerObject.position.y - speed, m_PlayerObject.position.z);
                m_PlayerAnimatorObject.position = new Vector3(m_PlayerAnimatorObject.position.x, m_PlayerAnimatorObject.position.y + speed / 3, m_PlayerAnimatorObject.position.z);
                if (m_PlayerObject.position.y <= m_NewPosition.y)
                {
                    m_OldY--;
                    DestinationReached();
                }
                break;
            case DIRECTION.Right:
                m_PlayerObject.position = new Vector3(m_PlayerObject.position.x + speed, m_PlayerObject.position.y, m_PlayerObject.position.z);
                m_PlayerAnimatorObject.position = new Vector3(m_PlayerAnimatorObject.position.x, m_PlayerAnimatorObject.position.y + speed / 2, m_PlayerAnimatorObject.position.z);
                if (m_PlayerObject.position.x >= m_NewPosition.x)
                {
                    DestinationReached();
                }
                break;
            case DIRECTION.Left:
                m_PlayerObject.position = new Vector3(m_PlayerObject.position.x - speed, m_PlayerObject.position.y, m_PlayerObject.position.z);
                m_PlayerAnimatorObject.position = new Vector3(m_PlayerAnimatorObject.position.x, m_PlayerAnimatorObject.position.y + speed / 2, m_PlayerAnimatorObject.position.z);
                if (m_PlayerObject.position.x <= m_NewPosition.x)
                {
                    DestinationReached();
                }
                break;
        }
    }

    void DestinationReached()
    {
        m_PlayerReference.SetDirection(DIRECTION.None);
        m_PlayerReference.SetMovementStatus(MOVEMENT_STATUS.MovingCombo);
        m_PlayerObject.position = m_NewPosition;
        m_PlayerAnimatorObject.position = new Vector3(m_PlayerAnimatorObject.position.x, m_OldY, m_PlayerAnimatorObject.position.z);
        m_MovementStarted = true;
    }
}
