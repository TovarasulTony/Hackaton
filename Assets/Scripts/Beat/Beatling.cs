using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BEATLING_PARITY
{
    Invalid,
    Left,
    Right
}

public class Beatling : MonoBehaviour
{
    BEATLING_PARITY m_Parity;
    Heart m_HeartReference = null;

    // Update is called once per frame
    void Update()
    {
        if (m_Parity == BEATLING_PARITY.Left)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * 5f, transform.position.y, transform.position.z);
            if (transform.position.x >= m_HeartReference.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * 5f, transform.position.y, transform.position.z);
            if (transform.position.x <= m_HeartReference.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetParity(BEATLING_PARITY _parity)
    {
        m_Parity = _parity;
    }

    public void SetHeartReference(Heart _destinationX)
    {
        Debug.Log(transform.position);
        m_HeartReference = _destinationX;
    }
}
