using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform myPlayer;

    void Start()
    {


    }


    void LateUpdate()
    {
        transform.position = new Vector3(myPlayer.position.x, myPlayer.position.y, myPlayer.position.z -20);
    }
}
