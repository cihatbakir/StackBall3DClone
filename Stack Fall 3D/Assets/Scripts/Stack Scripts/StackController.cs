using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour  // Zeminleri parçalarken  oluþacak animasyon
{

    [SerializeField]
    private StackPartController[] stackPartControlls = null;

    public int Lenght { get; internal set; }

    /*
    [HideInInspector]   // müfettiþi gizlemeyi ekledik.
    public bool test;      */

   /* private void OnCollisionEnter(Collision collision)
    {
         if(OnCollision)  //**\\ detaylý araþtýr, sonra
    }*/
    public void ShatterAllParts()  // ShatterAllParts = Tüm parçalarý parçala
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<Player>().IncreaseBrokenStacks(); // kýrýk yýðýný artýr
        }

        foreach (StackPartController o in stackPartControlls) // parça kontrolleri
        {
            o.Shatter();  // Zemin parçalandý.
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);  // zeminin teknoloji parçalandýktan 1 saniye sonra ileri doðru uzaklaþmasý

       /* foreach (StackPartController o in stackPartControlls) // parça kontrolleri
        {
            o.RemoveAllChilds();  // parçalanmayacak tamamen yok olacak.
        } */
        Destroy(gameObject);


    }

}
