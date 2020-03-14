using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject GoDown;
    public GameObject GoRight;
    public GameObject GoUp;
    public GameObject GoLeft;

    public float speed;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down");
           
            gameObject.transform.position = GoDown.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            Debug.Log("Up");
            gameObject.transform.position = GoUp.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.position = GoLeft.transform.position;
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.position = GoRight.transform.position;
            Debug.Log("Right");
        }
    }
}
