using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaster : MonoBehaviour
{
    private float m_TimePassed = 0;
    public bool m_TimeParity=false;
    private int deSters = 0;
    private bool mul = true;
    public Transform hcs;

    private const float c_WindupSong = 0.174f;
    private const int c_BeatTime = 5200;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play(0);

        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onBeat.AddListener(onOnbeatDetected);
        processor.onSpectrum.AddListener(onSpectrum);
    }
    void onOnbeatDetected()
    {
        if (mul == true)
        {
            mul = false;
            hcs.position = new Vector3(1f, 0f, 0f);
            Debug.Log("+++++++++++++++++");
        }
        else
        {
            mul = true;
            hcs.position = new Vector3(0f, 0f, 0f);
            Debug.Log("------------");
        }
    }

    //This event will be called every frame while music is playing
    void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        m_TimePassed += Time.deltaTime;

        int timeParity = ((int)((m_TimePassed - c_WindupSong) * 10000) / c_BeatTime)%2;
        if(deSters!= timeParity)
        {
            m_TimeParity = deSters == 0 ? true : false;
            deSters = timeParity;
        }*/
        //if (m_Tim)
    }
}
