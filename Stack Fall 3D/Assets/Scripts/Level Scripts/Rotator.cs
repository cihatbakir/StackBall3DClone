using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // 2 de�i�kenli tek sat�r kod  // bu kod zeminin hareketi i�in kullan�lmakta
    public float speed = 10;
   
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
