using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour, IBeat
{
    public Sprite m_ExpandedSprite;
    Sprite m_NormalSprite;

    void Start()
    {
        BeatMaster.instance.SubscribeToBeat(gameObject.GetComponent<IBeat>());
        m_NormalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void OnBeat()
    {
        Sprite obj_Sprite = GetComponent<Sprite>();
        StartCoroutine("HeartBeat");
    }

    IEnumerator HeartBeat()
    {
        GetComponent<SpriteRenderer>().sprite = m_ExpandedSprite;
        yield return new WaitForSeconds(.1f);
        GetComponent<SpriteRenderer>().sprite = m_NormalSprite;
    }
}
