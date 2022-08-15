using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour  // Score Y�neticisi
{
    public static ScoreManager instance; // instance = misal

    private Text scoreText;

    public int score = 10;  // skorumuzun 10 ile ba�lamas�
    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        MakeSingleton(); // MakeSingleton = yap�m 
    }
    void Start()
    {
       // scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        AddScore(0);
    }

    private void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text = score.ToString();

        }
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
        scoreText.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
    }
}
