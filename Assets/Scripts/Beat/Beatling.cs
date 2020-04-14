using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatling : MonoBehaviour
{
    bool m_Parity;

    // Update is called once per frame
    void Update()
    {
        if (m_Parity == false)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * 5, -3.5f, 1);
            if (transform.position.x >= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * 5, -3.5f, 1);
            if (transform.position.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetParity(bool _parity)
    {
        m_Parity = _parity;
    }
}
