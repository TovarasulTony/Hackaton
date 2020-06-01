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
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Hp_UI.instance.DrawHearts(1, 1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Hp_UI.instance.DrawHearts(3, 7);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Hp_UI.instance.DrawHearts(6.5f, 7);
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            Hp_UI.instance.DrawHearts(10, 10);
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            Hp_UI.instance.DrawHearts(1.5f, 10);
        }
    }
}
