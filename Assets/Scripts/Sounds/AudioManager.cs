using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectsMixer;

    public AudioSource playerDamage;

    public static AudioManager instance;

    [Range(-80, 10)]
    public float masterVol, effectsVol;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayAudio(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume();
        EffectsVolume();
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("masterVolume", masterVol);
    }

    public void EffectsVolume()
    {
        effectsMixer.SetFloat("effectsVolume", effectsVol);
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
