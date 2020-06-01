using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Icons;

    Dictionary<string, GameObject> m_IconDictionary;
    GameObject m_CurrentIcon;

    void Awake()
    {
        m_IconDictionary = new Dictionary<string, GameObject>();
        foreach(GameObject iconElement in m_Icons)
        {
            m_IconDictionary.Add(iconElement.name, iconElement);
        }
    }

    public void ChangeIcon(string _itemName)
    {
        if (_itemName == "none")
        {
            Destroy(m_CurrentIcon);
            m_CurrentIcon = null;
            return;
        }
        if (m_CurrentIcon != null)
        {
            Destroy(m_CurrentIcon);
        }
        m_CurrentIcon = Instantiate(m_IconDictionary[_itemName], transform);
    }
}
