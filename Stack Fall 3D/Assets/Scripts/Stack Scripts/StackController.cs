using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour  // Zeminleri par�alarken  olu�acak animasyon
{

    [SerializeField]
    private StackPartController[] stackPartControlls = null;

    public int Lenght { get; internal set; }

    /*
    [HideInInspector]   // m�fetti�i gizlemeyi ekledik.
    public bool test;      */

   /* private void OnCollisionEnter(Collision collision)
    {
         if(OnCollision)  //**\\ detayl� ara�t�r, sonra
    }*/
    public void ShatterAllParts()  // ShatterAllParts = T�m par�alar� par�ala
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<Player>().IncreaseBrokenStacks(); // k�r�k y���n� art�r
        }

        foreach (StackPartController o in stackPartControlls) // par�a kontrolleri
        {
            o.Shatter();  // Zemin par�aland�.
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);  // zeminin teknoloji par�aland�ktan 1 saniye sonra ileri do�ru uzakla�mas�

       /* foreach (StackPartController o in stackPartControlls) // par�a kontrolleri
        {
            o.RemoveAllChilds();  // par�alanmayacak tamamen yok olacak.
        } */
        Destroy(gameObject);


    }

}
