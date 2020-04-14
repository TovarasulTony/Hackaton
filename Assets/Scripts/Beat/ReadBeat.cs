using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBeat
{
    List<float> m_BeatList = new List<float>();
    // Start is called before the first frame update
    public ReadBeat()
    {
        int i = 0;
        string[] lines = System.IO.File.ReadAllLines(@"D:\Hackaton\Assets\Scripts\Beat\beat.txt");
        foreach (string line in lines)
        {
            m_BeatList.Add(float.Parse(line, System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            ++i;
        }
    }

    // Update is called once per frame
    public List<float> GetBeatList()
    {
        return m_BeatList;
    }
}
