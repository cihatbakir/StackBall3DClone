using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour  // Score Yöneticisi
{
    public static ScoreManager instance; // instance = misal
    public int score = 10;  // skorumuzun 10 ile baþlamasý
    void Awake()
    {
        MakeSingleton(); // MakeSingleton = yapým 
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
    //    if (Input.GetKeyDown(KeyCode.Space))   // Space ile skor artýþý
    //        score++;
    //}

   public void AddScore(int amount) // amount =tutar
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", score);
        print(score);
        // LoadTheText  = metin yükle
    }
    public void ResetScore()
    {
        score = 0;
    }
}
