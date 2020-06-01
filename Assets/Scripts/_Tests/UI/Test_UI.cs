using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            UI.instance.NotifyUIChange("attack", "none");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            UI.instance.NotifyUIChange("attack", "axe");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            UI.instance.NotifyUIChange("attack", "long_sword");
        }
    }
}
