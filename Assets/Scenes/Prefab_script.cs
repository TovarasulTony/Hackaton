using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_script : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> list;
    void Start()
    {
        Debug.Log("Exist");
        foreach(int element in list)
            Debug.Log(element);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
