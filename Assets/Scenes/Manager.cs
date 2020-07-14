using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Prefab_script prefab;
    void Start()
    {
        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
