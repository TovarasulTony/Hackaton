﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private List<Icon> m_BorderList;

    Dictionary<string, Icon> m_BorderDictionary;
    public static UI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Instanta exista deja!");
            Destroy(instance);
        }
        instance = GetComponent<UI>();
        m_BorderDictionary = new Dictionary<string, Icon>();
        foreach (Icon border in m_BorderList)
        {
            m_BorderDictionary.Add(border.name, border);
        }
        Transform rightSide = transform.Find("RightSide");
        rightSide.localPosition = new Vector3((float)Screen.width * 23 / 51200, 0, 0);//raport gasit de mine
        Transform leftSide = transform.Find("LeftSide");
        leftSide.localPosition = new Vector3(-((float)Screen.width * 23 / 25600), 0, 0);//raport gasit de mine
    }

    public void CameraShake()
    {
        GetComponent<CameraFollow>().CameraShake();
    }

    public void NotifyUIChange(string _uiElement, string _newElement)
    {
        m_BorderDictionary[_uiElement].ChangeIcon(_newElement);
    }
}
