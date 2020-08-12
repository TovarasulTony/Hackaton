using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : GenericBehavior, IBeat, IPlayerSubscriber
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

        m_PlayerReference.Subscribe(this);
        m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().SetFloat("jumpSpeed", 0.067f / 0.14f);
    }

    protected override void FixedUpdateMyBehavior()
    {
        m_MovementStatus = m_PlayerReference.GetMovementStatus();
        m_MovementDirection = m_PlayerReference.GetDirection();
        m_NewPosition = m_PlayerReference.GetNewPosition();
        MovingAnimation();
    }

    protected override void StartMyBehavior()
    {
        BeatMaster.instance.SubscribeToBeat(this);
    }

    public void OnPlayerMovement(DIRECTION _direction)
    {
        m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().SetTrigger("jump");
    }

    public void OnBeat()
    {
        //Debug.Log(0.065f / BeatMaster.instance.GetTimeToNextBeat());
        //Debug.Log(m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().speed);
        //Debug.Log((0.04f/BeatMaster.instance.GetTimeToNextBeat()));
        //Debug.Log(m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().speed);
        //Debug.Log(BeatMaster.instance.GetTimeToNextBeat());
        //m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().SetFloat("time", BeatMaster.instance.GetTimeToNextBeat());
        //m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().speed = 0.067f / BeatMaster.instance.GetTimeToNextBeat();
        m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().SetFloat("speed", 0.067f / BeatMaster.instance.GetTimeToNextBeat());
        //Debug.Log(m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().speed);
        m_PlayerReference.transform.Find("Animation").GetComponent<Animator>().SetTrigger("animation");
    }

    public void AttackAnimation(DIRECTION _attackDirection)
    {
        //mda ma cam pis pe cacatul asta de functie, o sa il fac minimal I guess
        Transform attackSwipe = Object.Instantiate(Library.instance.GetPrefab("AttackSwipe"));
        attackSwipe.position = m_PlayerObject.position;
        switch(_attackDirection)
        {
            case DIRECTION.Up:
                attackSwipe.position = new Vector3(attackSwipe.position.x, attackSwipe.position.y + 1.2f, attackSwipe.position.z);
                attackSwipe.transform.Rotate(0, 0, 90, Space.World);
                break;
            case DIRECTION.Down:
                attackSwipe.position = new Vector3(attackSwipe.position.x, attackSwipe.position.y - 0.8f, attackSwipe.position.z);
                attackSwipe.transform.Rotate(0, 0, -90, Space.World);
                break;
            case DIRECTION.Right:
                attackSwipe.position = new Vector3(attackSwipe.position.x + 0.8f, attackSwipe.position.y + 0.2f, attackSwipe.position.z);
                break;
            case DIRECTION.Left:
                attackSwipe.position = new Vector3(attackSwipe.position.x - 0.8f, attackSwipe.position.y + 0.2f, attackSwipe.position.z);
                attackSwipe.transform.Rotate(0, 0, 180, Space.World);
                break;
        }
        UI.instance.CameraShake();
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
