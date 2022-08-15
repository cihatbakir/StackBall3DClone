using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody rb;

    private float currentTime;  // �imdiki zaman

    private bool smash, invincible; // parampar�a etmek.  // invincible = yenilmez

    private int currentBrokenStacks, totalStack; // seviye art���ndaki g�sterge, current = mevcut stack, total = toplam y���m

    public GameObject invicnbleObj;// yenilmez objesi
    public Image invincibleFill; // yenilmezin alev s�resinin doldu�u alan
    public GameObject fireEffect , winEffect, splashEffect ;  // alev alma efekti


    public enum PlayerState // level art���nda oyuncunun durumu 
    {
        Prepare,    // Haz�rlama
        Playing,    // Oynamak
        Died,       // �l�
        Finish    // Biti�      
    }

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    public AudioClip bounceOffClip, deadClip, winClip, destoryClip, iDestroyClip;
     // ses klibi -> S��rama kapal� klip, �l�m klibi , galibiyet klibi
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenStacks = 0;
    }
    void Start()
    {
        totalStack = FindObjectsOfType<StackController>().Length;
     //    print(totalStack);
    }

   

    void Update()  // tek tek �al��t�racak
    {
        if(playerState == PlayerState.Playing)  // level art��� i�in
        {
            if (Input.GetMouseButtonDown(0))   // {} s�sl� parantez kullanabiliriz ama smash varken kullanmasakta olur 
                smash = true;

            if (Input.GetMouseButtonUp(0))
                smash = false;

            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;  // yenilmez isek  faha h�zl� zeminleri pa�alayacak ve h�zl� a�a�� inecek

                if (!fireEffect.activeInHierarchy)
                    fireEffect.SetActive(true); // efekti etkinle�mesi.

            }
            else
            {  // yenilmez olmad���m�z zaman 

                if (fireEffect.activeInHierarchy)
                    fireEffect.SetActive(false); // efektin kapanmas�

                if (smash)
                    currentTime += Time.deltaTime * .8f;
                else
                    currentTime -= Time.deltaTime * .5f;
            }
            // UI check = kontrol et

            if (currentTime >= 0.3f || invincibleFill.color == Color.red)  // Alev alma k�sm�
                invicnbleObj.SetActive(true);
            else
                invicnbleObj.SetActive(false);

            if (currentTime >= 1)
            {
                currentTime = 1;    // �imdiki zaman 1 ise yenilmez true(aktif)
                invincible = true;
                invincibleFill.color = Color.red;

            }
            else if (currentTime <= 0)
            {
                currentTime = 0;  // �imdiki zaman 0 ise yenilmez false(kapal�)
                invincible = false;
                invincibleFill.color = Color.white;
            }

            if (invicnbleObj.activeInHierarchy)  // alev animasyonu i�in artan s�reninin g�stergesi
                invincibleFill.fillAmount = currentTime / 1;

            // print(currentTime);
        }
   
         /*
         if (playerState == PlayerState.Prepare)      //***\\ buray� homeUI'dan sonra kald�rd�k ve GameUI i�erisinde ki void Update i�erisini yazd�k.
        {                                                       // prepare kullan�ld�
            if (Input.GetMouseButtonDown(0))
                playerState = PlayerState.Playing;
        } */
        
        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
                //  playerState = PlayerState.Playing;
                FindObjectOfType<LevelSpawner>().NextLevel();
        }

    }

    void FixedUpdate()  //sanide update g�re fark atar
    {
        if(playerState == PlayerState.Playing)  // Oyuncunun level art��� i�in
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
    
    public void IncreaseBrokenStacks() // k�r�k y���nlar� artt�r�n.
    {
        currentBrokenStacks++;

       
        if(!invincible)  // yenilmez aktif de�il iken 
       {
            ScoreManager.instance.AddScore(1);  // misal skor u bir art�r
            SoundManager.instance.PlaySoundFX(destoryClip, 1);
     //  FindObjectOfType<ScoreManager>().AddScore(1);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 1);
        }
    }
    void OnCollisionEnter(Collision target)     // target = hedef         // OnCollisionEnter Bas�l� tuttu�umuzda yap���yor
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            if (target.gameObject.tag != "Finish")
            {
                GameObject splash = Instantiate(splashEffect);
                splash.transform.SetParent(target.transform);
                splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
                float randomScale = Random.Range(0.18f, 0.25f);
                splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
                splash.transform.position = new Vector3(transform.position.x, transform.position.y - 0.22f, transform.position.z);
                splash.GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;

            }
            SoundManager.instance.PlaySoundFX(bounceOffClip, 1); // m�zi�in zaman�nda �almas�
        }
        else // bu komutta gelen nesne(d��manlar�) yok etmek i�in yaz�yoruz.
        {    // ama zemin hareket etmiyor
            if(invincible)  // Yenilmez iken zeminin h�zl� par�alanmas� ve tamam�n� yok etmesi.
            {
                if (target.gameObject.tag == "enemy"  || target.gameObject.tag == "plane")
                {
                    //  Destroy(target.transform.parent.gameObject); par�alanmay� ekledikten sonra buray� sildik.
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {

                if (target.gameObject.tag == "enemy")
                {
                    //   Destroy(target.gameObject); //objeyi yok et(tahrip etmek) sadece bulundu�u k�sm� yok eder.
                    // Destroy(target.transform.parent.gameObject); //buras� zeminin taman�n� yok eder */* par�alanmay� ekledikten sonra buray� sildik.
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();

                }
                if (target.gameObject.tag == "plane")  // burada ise siyah renkli zemine gelince durmas�n� sa�layan komut

                {
                    // Debug.Log("Game Over"); // eski hali
                    rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    playerState = PlayerState.Died;  // Died = ba�l�k

                    // print("Game Over");
                    ScoreManager.instance.ResetScore(); // skorun yenilenmesi.
                    SoundManager.instance.PlaySoundFX(deadClip, 1);  // �l�m m�zi�i
                }
            }
        }



        FindObjectOfType<GameUI>().LevelSliderFill(currentBrokenStacks / (float)totalStack);

        if (target.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;    // Level art��� Finish
            SoundManager.instance.PlaySoundFX(winClip, 1);  // win m�zi�i
            GameObject win = Instantiate(winEffect); // bitirme efekti
            win.transform.SetParent(Camera.main.transform);
            win.transform.localPosition = Vector3.up*1.5f;
            win.transform.eulerAngles = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision target)     // OnCollisionStay Geri eski hale gelmesini sa�l�yor.
    {
        if (!smash || target.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5 , 0);
        }
    }

}




