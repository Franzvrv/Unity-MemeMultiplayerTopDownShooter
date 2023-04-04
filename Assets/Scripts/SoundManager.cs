using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip Bang;
    static AudioSource audioSrc;

    void Start()
    {
        Bang = Resources.Load<AudioClip>("Bang");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void Playsound(string clip)
    {
        switch (clip)
        {
            case "Bang":
                audioSrc.PlayOneShot(Bang);
                break;
        }


    }
}
