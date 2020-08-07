using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_UI : MonoBehaviour
{
    [SerializeField]
    List<Transform> m_DigitsPrefab;
    Transform m_Digits;
    float c_SpaceBetweenDigits = 0.04f;

    public static Gold_UI instance;

    void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Instanta deja creata!");
            Destroy(instance);
        }
        instance = GetComponent<Gold_UI>();
        m_Digits = transform.Find("digits");
        DrawGold(0);
    }

    public void DrawGold(int _gold)
    {
        int digitPosition = 0;
        ClearDigits();
        Stack<int> digitStack = GetDigitStack(_gold);
        foreach(int digit in digitStack)
        {
            Transform digitUI = Instantiate(m_DigitsPrefab[digit]);
            digitUI.transform.parent = m_Digits;
            digitUI.transform.localPosition = new Vector3(0 + digitPosition * c_SpaceBetweenDigits, 0, 0);
            digitUI.transform.localScale = new Vector3(1, 1, 1);
            digitPosition++;
        }
    }

    Stack<int> GetDigitStack(int _gold)
    {
        Stack<int> digitStack = new Stack<int>();
        while(_gold > 0)
        {
            digitStack.Push(_gold % 10);
            _gold /= 10;
        }
        if(digitStack.Count == 0)
        {
            digitStack.Push(0);
        }
        return digitStack;
    }

    void ClearDigits()
    {
        foreach (Transform child in m_Digits)
        {
            Destroy(child.gameObject);
        }
    }
}
