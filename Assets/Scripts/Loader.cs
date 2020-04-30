using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public BeatMaster m_BeatMaster;
    public Player m_Player;
    public TileGenerator m_TileGenerator;
    public CameraFollow m_Camera;

    void Start()
    {
        m_BeatMaster = Instantiate(m_BeatMaster);
        m_TileGenerator = Instantiate(m_TileGenerator);
        m_Player = Instantiate(m_Player);
        m_Camera = Instantiate(m_Camera);

        m_Player.SetTileGenerator(m_TileGenerator);
        m_Camera.SetPlayerReference(m_Player);
    }
}
