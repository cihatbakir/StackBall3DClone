using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour  // Score Y�neticisi
{
    public static ScoreManager instance; // instance = misal
    public int score = 10;  // skorumuzun 10 ile ba�lamas�
    void Awake()
    {
        MakeSingleton(); // MakeSingleton = yap�m 
    }
    void Start()
    {
        AddScore(0);
    }

    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this; // this = bu
            DontDestroyOnLoad(gameObject);
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))   // Space ile skor art���
    //        score++;
    //}

   public void AddScore(int amount) // amount =tutar
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", score);
        print(score);
        // LoadTheText  = metin y�kle
    }
    public void ResetScore()
    {
        score = 0;
    }
}
