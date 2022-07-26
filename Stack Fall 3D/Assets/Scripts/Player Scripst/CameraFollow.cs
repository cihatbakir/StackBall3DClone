using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 camFollow; // kameran�n takibi
    private Transform player, win; // player ve win'i takip etmemizi sa�l�yor. 

    void Awake()
    {
        player = FindObjectOfType<Player>().transform;  // playeri tan�mlad�k.
    }


    void Update()
    {
        if (win == null)
            win = GameObject.Find("win(Clone)").GetComponent<Transform>(); // win'i null ba�lay�p win k�sm�na gelince durmas�n� sa�lar

        if (transform.position.y > player.transform.position.y && transform.position.y > win.position.y + 4.5F) // && �ncesinde y k�sm�nda hareketi ,,,
                                                                                                                // && sonras�nda ise en altta win e gelince kameran�n tam yerine oturmas� i�in .

            camFollow = new Vector3(transform.position.x, player.position.y, transform.position.z);   // vector3 ile oyuncu y konumunda transform ise x ve z ekseninde durmas�n� sa�l�yor

        transform.position = new Vector3(transform.position.x, camFollow.y, -5);  // burada ise camera y� -5 y eksninde a�a�� inmesini sa�l�yor.
    }
}

