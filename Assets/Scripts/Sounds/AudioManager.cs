using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectsMixer;

    public AudioSource playerDamage, BGMusic, skeletonHit, hellHoundHit, skullHit, spark, potion, ghostDamage, swampThingDamage, bossDamage;

    public static AudioManager instance;

    [Range(-80, 10)]
    public float masterVol, effectsVol;
    public Slider masterSlider, effectsSlider;

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
        PlayAudio(BGMusic);

        masterSlider.minValue = -80;
        masterSlider.maxValue = 10;

        effectsSlider.minValue = -80;
        effectsSlider.maxValue = 10;

        masterSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0);
    }

    public void MasterVolume()
    {
        DataManager.instance.MusicData(masterSlider.value);
        musicMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void EffectsVolume()
    {
        DataManager.instance.EffectsData(effectsSlider.value);
        effectsMixer.SetFloat("effectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopMusic(AudioSource audio)
    {

    }
}
