using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // 2 deðiþkenli tek satýr kod  // bu kod zeminin hareketi için kullanýlmakta
    public float speed = 10;

    void Update()
    {
        NewSpeed();

        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    void NewSpeed()
    {
        if (PlayerPrefs.GetInt("Level") > 20 && PlayerPrefs.GetInt("Level") <= 40)
        {
            speed = 102.5f;
        }
        else if (PlayerPrefs.GetInt("Level") > 40 && PlayerPrefs.GetInt("Level") <= 60)
        {
            speed = 107.5f;
        }
        else if (PlayerPrefs.GetInt("Level") > 60 && PlayerPrefs.GetInt("Level") <= 80)
        {
            speed = 110;
        }
        else if (PlayerPrefs.GetInt("Level") > 80 && PlayerPrefs.GetInt("Level") <= 100)
        {
            speed = 112.5f;
        }
        else if (PlayerPrefs.GetInt("Level") > 100 && PlayerPrefs.GetInt("Level") <= 150)
        {
            speed = 115;
        }
        else if (PlayerPrefs.GetInt("Level") > 150 && PlayerPrefs.GetInt("Level") <= 200)
        {
            speed = 120;
        }
        else if (PlayerPrefs.GetInt("Level") > 200 && PlayerPrefs.GetInt("Level") <= 250)
        {
            speed = 125;
        }
        else if (PlayerPrefs.GetInt("Level") > 250 && PlayerPrefs.GetInt("Level") <= 300)
        {
            speed = 127.5f;
        }
        else if (PlayerPrefs.GetInt("Level") > 300 && PlayerPrefs.GetInt("Level") <= 350)
        {
            speed = 130;
        }
        else if (PlayerPrefs.GetInt("Level") > 300 && PlayerPrefs.GetInt("Level") <= 350)
        {
            speed = 135;
        }
        else if (PlayerPrefs.GetInt("Level") > 350 && PlayerPrefs.GetInt("Level") <= 400)
        {
            speed = 140;
        }
        else if (PlayerPrefs.GetInt("Level") > 450 && PlayerPrefs.GetInt("Level") <= 500)
        {
            speed = 145;
        }
        else if (PlayerPrefs.GetInt("Level") > 500 && PlayerPrefs.GetInt("Level") <= 700)
        {
            speed = 150;
        }
        else if (PlayerPrefs.GetInt("Level") > 700)
        {
            speed = 170;
        }
    }
}

