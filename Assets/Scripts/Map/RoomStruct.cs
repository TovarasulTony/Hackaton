using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStruct
{
    public int x;
    public int y;
    public int width;
    public int heigth;

    public RoomStruct()
    {
        heigth = Random.Range(4, 6);
        width = Random.Range(4, 7);
    }
}
