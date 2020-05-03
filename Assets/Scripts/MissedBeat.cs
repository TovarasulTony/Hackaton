using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBeat : MonoBehaviour
{
    float m_InitY;
    void Start()
    {
        //Chestia asta e super urata... stiu
        Vector3 position = ((FindObjectsOfType<Heart>())[0]).transform.position;
        transform.position = new Vector3(position.x, position.y + 1, position.z + 0.01f);
        m_InitY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 2, transform.position.z + 0.01f);
        if(transform.position.y >= m_InitY + 2)
        {
            Destroy(gameObject);
        }
    }
}
