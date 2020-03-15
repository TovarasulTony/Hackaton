using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void Dig()
    {
        Destroy(gameObject);
    }
}
