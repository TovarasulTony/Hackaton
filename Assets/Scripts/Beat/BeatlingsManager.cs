using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatlingsManager : MonoBehaviour
{
    public Beatling m_BeatlingPrefab;
    List<float> m_BeatsList = null;
    int m_CurrentBeat = 0;
    float m_Time = 0;
    float m_TimeFixed = 0;

    void Start()
    {
        m_BeatsList = BeatMaster.instance.GetBeatsList();
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        InstantiateBeatling();
    }

    private void FixedUpdate()
    {
        m_TimeFixed += Time.deltaTime;
        InstantiateBeatling();
    }

    void InstantiateBeatling()
    {
        if(m_BeatsList[m_CurrentBeat] - m_Time > 5 || m_BeatsList[m_CurrentBeat] - m_TimeFixed > 5f)
        {
            return;
        }

        float beatling_time = m_Time > m_TimeFixed ? m_Time : m_TimeFixed;
        float beatling_distance = m_BeatsList[m_CurrentBeat] - beatling_time;
        Beatling newBeattlingLeft = Instantiate(m_BeatlingPrefab);
        newBeattlingLeft.transform.position = new Vector3(-(beatling_distance * 5), -3.5f, 1);
        newBeattlingLeft.SetParity(false);

        Beatling newBeattlingRight = Instantiate(m_BeatlingPrefab);
        newBeattlingRight.transform.position = new Vector3(beatling_distance * 5, -3.5f, 1);
        newBeattlingRight.SetParity(true);

        m_CurrentBeat++;
    }
}
