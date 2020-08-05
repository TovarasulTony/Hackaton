using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public SoundManager m_SoundManager;
    public Library m_Library;
    public BeatMaster m_BeatMaster;
    public Player m_Player;
    public Map m_Map;
    public CameraFollow m_Camera;

    public List<MonoBehaviour> m_Tests;

    void Start()
    {
        m_SoundManager = Instantiate(m_SoundManager);
        m_Library = Instantiate(m_Library);
        m_BeatMaster = Instantiate(m_BeatMaster);
        m_Map = Instantiate(m_Map);
        m_Player = Instantiate(m_Player);
        m_Camera = Instantiate(m_Camera);
        
        m_Camera.SetPlayerReference(m_Player);
        m_Map.SetPlayerReference(m_Player);

        foreach(MonoBehaviour test in m_Tests)
        {
            Instantiate(test);
        }
    }
}
