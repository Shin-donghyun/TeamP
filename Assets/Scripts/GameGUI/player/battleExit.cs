using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleExit : MonoBehaviour
{
    public GameObject Enmeyimg;
   
    private int count = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            Debug.Log(count);
            if (count == 10)
            {
                Enmeyimg.SetActive(true);
            }
        }
    }
}
