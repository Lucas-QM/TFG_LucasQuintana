using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Ha entrado");
        AudioManager.instance.BGMusic.Stop();
        AudioManager.instance.BGMusic = audio;
        AudioManager.instance.PlayAudio(AudioManager.instance.BGMusic);
    }
}
