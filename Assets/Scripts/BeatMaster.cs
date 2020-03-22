using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaster : MonoBehaviour
{
    private float m_TimePassed = 0;
    public bool m_TimeParity=false;
    private int deSters = 0;

    private const float c_WindupSong = 0.174f;
    private const int c_BeatTime = 5193;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play(0);

    }

    // Update is called once per frame
    void Update()
    {
        m_TimePassed += Time.deltaTime;

        int timeParity = ((int)((m_TimePassed - c_WindupSong) * 10000) / c_BeatTime)%2;
        if(deSters!= timeParity)
        {
            m_TimeParity = deSters == 0 ? true : false;
            deSters = timeParity;
        }
        //if (m_Tim)
    }
}
