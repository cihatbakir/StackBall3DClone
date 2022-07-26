using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // instance = misal
    private AudioSource audioSource;
    public bool sound = true;
    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
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

    public void  PlaySoundFX(AudioClip clip, float voulme)
    {
      /*float pitch = 1;  // pitch = saha    // sesi ayarlamamýz gerekiyordu kaldýrdýk 
        audioSource.pitch = pitch;  */   
        if (sound)
            audioSource.PlayOneShot(clip, voulme);
    }
}
