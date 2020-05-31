using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletorAnimation : GenericBehavior, IBeat
{
    Skeletor m_SkeletorReference;
    bool m_IsMovingAnimation = false;
    float m_OldY;
    DIRECTION m_MovementDirection;
    Transform m_SkeletorObject;
    Transform m_SkeletorAnimatorObject;
    Vector3 m_NewPosition;

    public void SetSkeletorReference(Skeletor _reference)
    {
        m_SkeletorReference = _reference;
        m_SkeletorObject = m_SkeletorReference.transform;
        m_SkeletorAnimatorObject = m_SkeletorObject.Find("Animation");

        BeatMaster.instance.SubscribeToBeat(this);
        m_SkeletorAnimatorObject.GetComponent<Animator>().SetFloat("speed", 0.333f / 0.5f);
    }

    public void OnBeat()
    {
        m_SkeletorAnimatorObject.GetComponent<Animator>().SetFloat("speed", 0.333f / BeatMaster.instance.GetTimeToNextBeat());
        m_SkeletorAnimatorObject.GetComponent<Animator>().SetTrigger("animation");

        if (m_SkeletorReference.GetState() == SKELETOR_STATE.Move)
        {
            return;
        }

        m_OldY = m_SkeletorAnimatorObject.position.y;
        m_IsMovingAnimation = true;
        m_NewPosition = m_SkeletorReference.GetNewPosition();
    }

    protected override void FixedUpdateMyBehavior()
    {
        m_MovementDirection = m_SkeletorReference.GetDirection();
        MovingAnimation();
    }

    void MovingAnimation()
    {
        if (m_IsMovingAnimation != true)
        {
            return;
        }
        float speed = Time.fixedDeltaTime * 7;
        switch (m_MovementDirection)
        {
            case DIRECTION.Up:
                m_SkeletorObject.position = new Vector3(m_SkeletorObject.position.x, m_SkeletorObject.position.y + speed, m_SkeletorObject.position.z);
                m_SkeletorAnimatorObject.position = new Vector3(m_SkeletorAnimatorObject.position.x, m_SkeletorAnimatorObject.position.y + speed / 3, m_SkeletorAnimatorObject.position.z);
                if (m_SkeletorObject.position.y >= m_NewPosition.y)
                {
                    m_OldY++;
                    DestinationReached();
                }
                break;
            case DIRECTION.Down:
                m_SkeletorObject.position = new Vector3(m_SkeletorObject.position.x, m_SkeletorObject.position.y - speed, m_SkeletorObject.position.z);
                m_SkeletorAnimatorObject.position = new Vector3(m_SkeletorAnimatorObject.position.x, m_SkeletorAnimatorObject.position.y + speed / 3, m_SkeletorAnimatorObject.position.z);
                if (m_SkeletorObject.position.y <= m_NewPosition.y)
                {
                    m_OldY--;
                    DestinationReached();
                }
                break;
            case DIRECTION.Right:
                m_SkeletorObject.position = new Vector3(m_SkeletorObject.position.x + speed, m_SkeletorObject.position.y, m_SkeletorObject.position.z);
                m_SkeletorAnimatorObject.position = new Vector3(m_SkeletorAnimatorObject.position.x, m_SkeletorAnimatorObject.position.y + speed / 2, m_SkeletorAnimatorObject.position.z);
                if (m_SkeletorObject.position.x >= m_NewPosition.x)
                {
                    DestinationReached();
                }
                break;
            case DIRECTION.Left:
                m_SkeletorObject.position = new Vector3(m_SkeletorObject.position.x - speed, m_SkeletorObject.position.y, m_SkeletorObject.position.z);
                m_SkeletorAnimatorObject.position = new Vector3(m_SkeletorAnimatorObject.position.x, m_SkeletorAnimatorObject.position.y + speed / 2, m_SkeletorAnimatorObject.position.z);
                if (m_SkeletorObject.position.x <= m_NewPosition.x)
                {
                    DestinationReached();
                }
                break;
        }
    }

    void DestinationReached()
    {
        Debug.Log("Reached");
        m_SkeletorObject.position = m_NewPosition;
        m_SkeletorAnimatorObject.position = new Vector3(m_SkeletorAnimatorObject.position.x, m_OldY, m_SkeletorAnimatorObject.position.z);
        m_IsMovingAnimation = false;
    }
}
