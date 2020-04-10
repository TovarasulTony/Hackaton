using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    bool switched = false;
    float time = 0;
    List<float> myList = new List<float>();
    int index = 0;
    // Start is called before the first frame update
   /*   
 15.51092971 16.02176871 16.55582766 17.06666667 17.60072562 18.11156463
 18.64562358 19.15646259 19.69052154 20.20136054 20.7354195  21.2462585
 21.78031746 22.29115646 22.82521542 23.33605442 23.87011338 24.38095238
 24.89179138 25.33297052
 */
    void Start()
    {
        myList.Add(0.092870f);
        myList.Add(0.62693878f);
        myList.Add(1.16099773f);
        myList.Add(1.67183673f);
        myList.Add(2.20589569f);
        myList.Add(2.71673469f);
        myList.Add(3.25079365f);
        myList.Add(3.76163265f);
        myList.Add(4.29569161f);
        myList.Add(4.80653061f);
        myList.Add(5.34058957f);
        myList.Add(5.85142857f);
        myList.Add(6.36226757f);
        myList.Add(6.89632653f);
        myList.Add(7.40716553f);
        myList.Add(7.94122449f);
        myList.Add(8.45206349f);
        myList.Add(8.91646259f);
        myList.Add(9.38086168f);
        myList.Add(10.30965986f);
        myList.Add(10.82049887f);
        myList.Add(11.33133787f);
        myList.Add(11.86539683f);
        myList.Add(12.37623583f);
        myList.Add(12.88707483f);
        myList.Add(13.42113379f);
        myList.Add(13.95519274f);
        myList.Add(14.46603175f);
        myList.Add(14.97687075f);
    }
    void Switch()
    {
        if(switched)
        {
            switched = false;
            transform.position = new Vector3(2, 2, -19);
        }
        else
        {
            switched = true;
            transform.position = new Vector3(0, 0, -19);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if(time >= myList[index])
        {
            index++;
            Switch();
        }
    }
}
