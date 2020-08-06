using System.Collections;
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
        if(instance != null)
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
