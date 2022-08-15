using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // 2 deðiþkenli tek satýr kod  // bu kod zeminin hareketi için kullanýlmakta
    public float speed = 10;
   
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
