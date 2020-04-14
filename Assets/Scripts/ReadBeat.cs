using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBeat : MonoBehaviour
{
    List<float> m_BeatList = new List<float>();
    int m_CurrentBeat = 0;
    float m_Time = 0;
    bool m_Parity = false;

    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Andreea\Desktop\Hackaton\Assets\Scripts\Beat\beat.txt");
        foreach (string line in lines)
        {
            m_BeatList.Add(float.Parse(line, System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            ++i;
        }
        audioS = GetComponent<AudioSource>();
        audioS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time>m_BeatList[m_CurrentBeat])
        {
            m_CurrentBeat++;
            Switch();
        }
    }

    void Switch()
    {
        if (m_Parity == false)
        {
            transform.position = new Vector3(10, 0, 0);
            m_Parity = true;
        }
        else
        {
            transform.position = new Vector3(-10, 0, 0);
            m_Parity = false;
        }
    }
}
