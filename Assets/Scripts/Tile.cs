using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile m_TileUp;
    public Tile m_TileDown;
    public Tile m_TileLeft;
    public Tile m_TileRight;
    public Wall m_Wall=null;

    public bool m_Parity;
    public BeatMaster m_BeatMaster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((transform));
        if(m_Parity == m_BeatMaster.m_TimeParity)
        {
            if (m_Parity == true)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 0, 255, 1);
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 255, 255, 1);
            }
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
    }
}
