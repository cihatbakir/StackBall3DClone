using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour

// Topumuzun alt�na gelecek zeminin otamaik olu�mas� i�in olu�turdu�umuz komut listesi(kod)
{
    public GameObject[] model;  // model nesnemiz
    [HideInInspector] // fonsiyonu gizlemek i�in
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;

    private GameObject temp1, temp2; // ge�ici 2 �zelli�e sahip olaca��z sonra d�ng� haline d�n��ecek


    public int level = 1, addOn = 7;  // halka say�s� i�in
    float i = 0;

    public Material plateMat, baseMat;
    public MeshRenderer playerMesh;


    void Awake() // Start't� de�i�tirip Awake //** seviye art��� i�in�n g�sterisi i�in,u� 
    {
        plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        baseMat.color = plateMat.color + Color.gray;
        playerMesh.material.color = plateMat.color;


        level = PlayerPrefs.GetInt("Level", 1);  // seviye atlama

        if (level > 9)    //   burada 9 levele kadar olup olmad���n� kontrol edebiliriz.
            addOn = 0;

        ModelSelection();  // �e�im e girmeden �nce ModelSelection u �a��rmam�z gerekli... 
        float random = Random.value;
        for (i = 0; i > -level - addOn; i -= 0.5f) // senaryo s�recince tekrar edecek.
        {

            if (level <= 20)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]);
            if (level > 20 && level <= 50)
                temp1 = Instantiate(modelPrefab[Random.Range(1, 3)]);
            if (level > 50 && level <= 100)
                temp1 = Instantiate(modelPrefab[Random.Range(2, 4)]);
            if (level > 100)
                temp1 = Instantiate(modelPrefab[Random.Range(3, 4)]);

            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0); // d�ng�

            if (Mathf.Abs(i) >= level * .3f && Mathf.Abs(i) <= level * .6f)  // d�z bir s�ra halinde de�ilde farkl� konumlarda engellerin gelmesi i�in // if in i�erisinde 8 ile 15 aras�nda
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;  // engelin bulundu�u konumdan 180 derece kar��ya ge�mesini sa�l�yor.
            } else if (Mathf.Abs(i) >= level * .8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0); // 50 level �st�ne ��kt�ktan sonra fark edilecek seviyeye ula��l�yor.
                if (random > .75f)
                {
                    temp1.transform.eulerAngles += Vector3.up * 180;
                }
            }

            temp1.transform.parent = FindObjectOfType<Rotator>().transform;// burada zeminin d�nmesinde kulland���m�z objenin tan�mlanmas�.
        }

        temp2 = Instantiate(winPrefab, new Vector3(0, i - 0.01f, 0), Quaternion.identity); // win en alt tabakaya inmesi demek.
        // temp2.transform.position = new Vector3(0, i - 0.01f, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            playerMesh.material.color = plateMat.color;
        }
    }

    void ModelSelection()  // renkleri ve �ekilleri random sekilde atamas�n� sa�layan komut.
    {
        int randomModel = Random.Range(0, 5);  // random olarak s�ras� ile �e�ilen model komutu
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i];
                break;

            case 1:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 4];
                break;

            case 2:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 8];
                break;


            case 3:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 12];
                break;


            case 4:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 16]; // Her birinde +4 eklemizin sebebi model scriptinde t�m elementleri yerle�tirmemiz.
                break;

        }

    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }


}
