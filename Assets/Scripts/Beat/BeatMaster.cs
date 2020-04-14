using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaster : MonoBehaviour
{
    public static BeatMaster instance = null;
    public AudioSource m_Audio = null;
    List<float> m_BeatsList = null;
    List<IBeat> m_ObserverList = null;

    //Beat
    int m_CurrentBeat = 0;
    float m_Time = 0;
    float m_TimeFixed = 0;

    private void Awake()
    {
        if(BeatMaster.instance != null)
        {
            Debug.LogWarning("BeatMaster instantiat de doua ori");
            Destroy(gameObject);
        }
        m_ObserverList = new List<IBeat>();
        instance = gameObject.GetComponent<BeatMaster>();
        m_Audio = GetComponent<AudioSource>();
        ReadBeat beatReader = new ReadBeat();
        m_BeatsList = beatReader.GetBeatList();
    }

    private void Start()
    {
        m_Audio.Play();
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        HandleBeat();
    }

    private void FixedUpdate()
    {
        m_TimeFixed += Time.deltaTime;
        HandleBeat();
    }

    void HandleBeat()
    {
        if(m_Time < m_BeatsList[m_CurrentBeat] || m_TimeFixed < m_BeatsList[m_CurrentBeat])
        {
            return;
        }

        m_CurrentBeat++;

        foreach(IBeat subscrieber in m_ObserverList)
        {
            subscrieber.OnBeat();
        }
    }

    public void SubscribeToBeat(IBeat _subscriber)
    {
        m_ObserverList.Add(_subscriber);
    }

    public List<float> GetBeatsList()
    {
        return m_BeatsList;
    }
}
