using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartController : MonoBehaviour // y���n par�as� denetleyici 
{
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private StackController stackController;
    private Collider collider;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();  // GetComponent = bile�en al
        meshRenderer = GetComponent<MeshRenderer>();
        stackController = transform.parent.GetComponent<StackController>();
        collider = GetComponent<Collider>();
    }
    public void Shatter() // Shatter = k�rmak
    {
        rigidBody.isKinematic = false;    // �lmeden �nce yer �ekimi etkinle�ecek
        collider.enabled = false;  // enabled = etkinle�tirilmi�

        Vector3 forcePoint = transform.parent.position;  // forcePoint = kuvvet noktas�
        float paretXpos = transform.parent.position.x;  // ana pozisyonunu = paret 
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subDir = (paretXpos - xPos < 0) ? Vector3.right : Vector3.left;

        /*  if (paretXpos - xPos < 0)
             subDir = Vector3.right;                   bir �st sat�rda yaz�lan kodun ayn�s� ama if d�ng�s�nde 
         else
             subDir = Vector3.left; */



        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35); // force = k�rmak  
        float torque = Random.Range(110, 180); // tork a��s�n�n 110 ile 180 derece aras�nda de�i�mesi

        rigidBody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rigidBody.AddTorque(Vector3.left * torque);
        rigidBody.velocity = Vector3.down;

    }

    public void RemoveAllChilds() // t�m �ocuklar� kald�r.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }

}
