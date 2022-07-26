using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;

    private float currentTime;  // þimdiki zaman

    private bool smash, invincible; // paramparça etmek.  // invincible = yenilmez


    public enum PlayerState // level artýþýnda oyuncunun durumu 
    {
        Prepare,    // Hazýrlama
        Playing,    // Oynamak
        Died,       // Ölü
        Finish,     // Bitiþ      
    }

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    public AudioClip bounceOffClip, deadClip, winClip, destoryClip, iDestroyClip;
     // ses klibi -> Sýçrama kapalý klip, ölüm klibi , galibiyet klibi
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()  // tek tek çalýþtýracak
    {
        if(playerState == PlayerState.Playing)  // level artýþý için
        {
            if (Input.GetMouseButtonDown(0))   // {} süslü parantez kullanabiliriz ama smash varken kullanmasakta olur 
                smash = true;

            if (Input.GetMouseButtonUp(0))
                smash = false;

            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;  // yenilmez isek  faha hýzlý zeminleri paçalayacak ve hýzlý aþaðý inecek
            }
            else
            {  // yenilmez olmadýðýmýz zaman 
                if (smash)
                    currentTime += Time.deltaTime * .8f;
                else
                    currentTime -= Time.deltaTime * .5f;
            }
            // UI check = kontrol et

            if (currentTime >= 1)
            {
                currentTime = 1;    // þimdiki zaman 1 ise yenilmez true(aktif)
                invincible = true;

            }
            else if (currentTime <= 0)
            {
                currentTime = 0;  // þimdiki zaman 0 ise yenilmez false(kapalý)
                invincible = false;
            }
            print(currentTime);
        }
   
        if (playerState == PlayerState.Prepare)
        {                                                       // prepare kullanýldý
            if (Input.GetMouseButtonDown(0))
                playerState = PlayerState.Playing;
        }  
        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
                //  playerState = PlayerState.Playing;
                FindObjectOfType<LevelSpawner>().NextLevel();
        }

    }

    void FixedUpdate()  //sanide update göre fark atar
    {
        if(playerState == PlayerState.Playing)  // Oyuncunun level artýþý için
        {
            if (Input.GetMouseButton(0))
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }
        if (rb.velocity.y > 5)
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
    }
    
    public void IncreaseBrokenStacks() // kýrýk yýðýnlarý arttýrýn.
    {
        if(!invincible)  // yenilmez aktif deðil iken 
       {
            ScoreManager.instance.AddScore(1);  // misal skor u bir artýr
            SoundManager.instance.PlaySoundFX(destoryClip, 1);
     //  FindObjectOfType<ScoreManager>().AddScore(1);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 1);
        }
    }
    void OnCollisionEnter(Collision target)     // target = hedef         // OnCollisionEnter Basýlý tuttuðumuzda yapýþýyor
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.PlaySoundFX(bounceOffClip, 1); // müziðin zamanýnda çalmasý
        }
        else // bu komutta gelen nesne(düþmanlarý) yok etmek için yazýyoruz.
        {    // ama zemin hareket etmiyor
            if(invincible)  // Yenilmez iken zeminin hýzlý parçalanmasý ve tamamýný yok etmesi.
            {
                if (target.gameObject.tag == "enemy"  || target.gameObject.tag == "plane")
                {
                    //  Destroy(target.transform.parent.gameObject); parçalanmayý ekledikten sonra burayý sildik.
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {

                if (target.gameObject.tag == "enemy")
                {
                    //   Destroy(target.gameObject); //objeyi yok et(tahrip etmek) sadece bulunduðu kýsmý yok eder.
                    // Destroy(target.transform.parent.gameObject); //burasý zeminin tamanýný yok eder */* parçalanmayý ekledikten sonra burayý sildik.
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();

                }
                if (target.gameObject.tag == "plane")  // burada ise siyah renkli zemine gelince durmasýný saðlayan komut

                {
                    // Debug.Log("Game Over"); // eski hali
                    print("Game Over");
                    ScoreManager.instance.ResetScore(); // skorun yenilenmesi.
                    SoundManager.instance.PlaySoundFX(deadClip, 1);  // ölüm müziði
                }
            }
        }

        if(target.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;    // Level artýþý Finish
            SoundManager.instance.PlaySoundFX(winClip, 1);  // win müziði
        }
    }

    void OnCollisionStay(Collision target)     // OnCollisionStay Geri eski hale gelmesini saðlýyor.
    {
        if (!smash || target.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }

}




