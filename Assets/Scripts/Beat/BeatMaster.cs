using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BEAT_PARITY
{
    Invalid,
    Odd,
    Even
}

public class BeatMaster : MonoBehaviour
{
    public static BeatMaster instance = null;
    public AudioSource m_Audio = null;
    List<float> m_BeatsList = null;
    List<IBeat> m_ObserverList = null;

    //Beat
    BEAT_PARITY m_BeatParity;
    int m_CurrentBeat = 0;
    float m_TimeToNextBeat = 0;
    //float m_Time = 0;
    float m_TimeFixed = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("BeatMaster instantiat de doua ori");
            Destroy(gameObject);
        }
        m_ObserverList = new List<IBeat>();
        instance = gameObject.GetComponent<BeatMaster>();
        m_Audio = GetComponent<AudioSource>();
        ReadBeat beatReader = new ReadBeat();
        m_BeatsList = beatReader.GetBeatList();

        m_BeatParity = BEAT_PARITY.Even;
    }

    private void Start()
    {
        m_Audio.Play();
        m_Audio.volume = 0.1f;
    }
    /*
    void Update()
    {
        m_Time += Time.deltaTime;
        HandleBeat();
    }*/

    private void FixedUpdate()
    {
        m_TimeFixed += Time.fixedDeltaTime;
        HandleBeat();
    }

    void HandleBeat()
    {//m_Time < m_BeatsList[m_CurrentBeat] ||
        if ( m_TimeFixed < m_BeatsList[m_CurrentBeat])
        {
            return;
        }
        m_CurrentBeat++;
        m_TimeToNextBeat = m_BeatsList[m_CurrentBeat] - m_BeatsList[m_CurrentBeat - 1];
        ChangeBeatParity();

        foreach (IBeat subscrieber in m_ObserverList)
        {
            subscrieber.OnBeat();
        }
    }

    public void SubscribeToBeat(IBeat _subscriber)
    {
        m_ObserverList.Add(_subscriber);
    }

    public void UnsubscribeToBeat(IBeat _subscriber)
    {
        m_ObserverList.Remove(_subscriber);
    }

    public List<float> GetBeatsList()
    {
        return m_BeatsList;
    }

    public BEAT_PARITY GetBeatParity()
    {
        return m_BeatParity;
    }

    void ChangeBeatParity()
    {
        m_BeatParity = m_BeatParity == BEAT_PARITY.Even ? BEAT_PARITY.Odd : BEAT_PARITY.Even;
    }

    public int GetBeatNumber()
    {
        return m_CurrentBeat;
    }

    public float GetTimeToNextBeat()
    {
        return m_TimeToNextBeat;
    }
}
