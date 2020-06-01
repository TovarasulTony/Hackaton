using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_UI : MonoBehaviour
{
    [SerializeField]
    List<Transform> m_HeartPrefab;
    List<Transform> m_Hearts;
    Dictionary<string, Transform> m_PrefabDictionary;
    float c_Decrement = 1.2f;

    public static Hp_UI instance;

    private void Awake()
    {
        m_Hearts = new List<Transform>();
        m_PrefabDictionary = new Dictionary<string, Transform>();

        foreach(Transform prefab in m_HeartPrefab)
        {
            m_PrefabDictionary.Add(prefab.name, prefab);
        }

        if (instance != null)
        {
            Debug.LogWarning("Instanta deja creata!");
            Destroy(instance);
        }
        instance = GetComponent<Hp_UI>();
    }

    public void DrawHearts(float _currentHP, int _totalHP)
    {
        foreach(Transform heart in m_Hearts)
        {
            Destroy(heart.gameObject);
        }
        m_Hearts.Clear();
        float positionX = 2.4f;
        float positionY = 0.5f;
        string heartName = "hp_full";
        for (int i = 1; i <= _totalHP; ++i)
        {
            if (i - 0.5f == _currentHP)
            {
                heartName = "hp_half";
            }
            Transform heart = Instantiate(m_PrefabDictionary[heartName], transform);
            heart.localPosition = new Vector3(positionX, positionY, 0);
            m_Hearts.Add(heart);

            positionX -= c_Decrement;
            if (i == 5)
            {
                positionX = 2.4f;
                positionY = -0.5f;
            }
            if (i >= _currentHP)
            {
                heartName = "hp_empty";
            }
        }
    }
}
