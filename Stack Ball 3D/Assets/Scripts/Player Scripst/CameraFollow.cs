using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 camFollow; // kameranýn takibi
    private Transform player, win; // player ve win'i takip etmemizi saðlýyor. 

    void Awake()
    {
        player = FindObjectOfType<Player>().transform;  // playeri tanýmladýk.
    }


    void Update()
    {
        if (win == null)
            win = GameObject.Find("win(Clone)").GetComponent<Transform>(); // win'i null baðlayýp win kýsmýna gelince durmasýný saðlar

        if (transform.position.y > player.transform.position.y && transform.position.y > win.position.y + 4.5F) // && öncesinde y kýsmýnda hareketi ,,,
                                                                                                                // && sonrasýnda ise en altta win e gelince kameranýn tam yerine oturmasý için .

            camFollow = new Vector3(transform.position.x, player.position.y, transform.position.z);   // vector3 ile oyuncu y konumunda transform ise x ve z ekseninde durmasýný saðlýyor

        transform.position = new Vector3(transform.position.x, camFollow.y, -5);  // burada ise camera yý -5 y eksninde aþaðý inmesini saðlýyor.
    }
}

