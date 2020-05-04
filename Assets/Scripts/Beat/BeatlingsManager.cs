using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatlingsManager : MonoBehaviour
{
    public Beatling m_BeatlingPrefab;
    public Heart m_HeartReference;
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
        PrepareBeatling();
    }

    private void FixedUpdate()
    {
        m_TimeFixed += Time.fixedDeltaTime;
        PrepareBeatling();
    }

    void PrepareBeatling()
    {
        if(m_BeatsList[m_CurrentBeat] - m_Time > 5 || m_BeatsList[m_CurrentBeat] - m_TimeFixed > 5f)
        {
            return;
        }


        float max_time = m_Time > m_TimeFixed ? m_Time : m_TimeFixed;
        float beatling_distance = m_BeatsList[m_CurrentBeat] - max_time;

        InstantiateBeatling(BEATLING_PARITY.Left, m_HeartReference.transform.position.x - (beatling_distance * 5));
        InstantiateBeatling(BEATLING_PARITY.Right, m_HeartReference.transform.position.x + (beatling_distance * 5));

        m_CurrentBeat++;
    }

    void InstantiateBeatling(BEATLING_PARITY _beatlingParity, float _xPosition)
    {
        Beatling newBeattling = Instantiate(m_BeatlingPrefab);
        newBeattling.transform.position = new Vector3(_xPosition, m_HeartReference.transform.position.y, m_HeartReference.transform.position.z);
        newBeattling.SetParity(_beatlingParity);
        newBeattling.SetHeartReference(m_HeartReference);
        newBeattling.transform.SetParent(m_HeartReference.transform);
    }
}
