using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartController : MonoBehaviour // yýðýn parçasý denetleyici 
{
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private StackController stackController;
    private Collider collider;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();  // GetComponent = bileþen al
        meshRenderer = GetComponent<MeshRenderer>();
        stackController = transform.parent.GetComponent<StackController>();
        collider = GetComponent<Collider>();
    }
    public void Shatter() // Shatter = kýrmak
    {
        rigidBody.isKinematic = false;    // ölmeden önce yer çekimi etkinleþecek
        collider.enabled = false;  // enabled = etkinleþtirilmiþ

        Vector3 forcePoint = transform.parent.position;  // forcePoint = kuvvet noktasý
        float paretXpos = transform.parent.position.x;  // ana pozisyonunu = paret 
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subDir = (paretXpos - xPos < 0) ? Vector3.right : Vector3.left;

        /*  if (paretXpos - xPos < 0)
             subDir = Vector3.right;                   bir üst satýrda yazýlan kodun aynýsý ama if döngüsünde 
         else
             subDir = Vector3.left; */



        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35); // force = kýrmak  
        float torque = Random.Range(110, 180); // tork açýsýnýn 110 ile 180 derece arasýnda deðiþmesi

        rigidBody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rigidBody.AddTorque(Vector3.left * torque);
        rigidBody.velocity = Vector3.down;

    }

    public void RemoveAllChilds() // tüm çocuklarý kaldýr.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }

}
