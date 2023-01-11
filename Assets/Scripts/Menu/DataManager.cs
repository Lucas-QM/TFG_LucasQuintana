using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void EffectsData(float value)
    {
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }

    public void HealthData(float value)
    {
        PlayerPrefs.SetFloat("Health", value);
    }

    public void ManaData(float value)
    {
        PlayerPrefs.SetFloat("Mana", value);
    }

    public void SavePlayerPosition(float valueX, float valueY, int scene)
    {
        PlayerPrefs.SetFloat("PositionX", valueX);
        PlayerPrefs.SetFloat("PositionY", valueY);
        PlayerPrefs.SetInt("ActualScene", scene);
    }

    public void Level1MecanismData(int value)
    {
        PlayerPrefs.SetInt("level1Mecanism", value);
    }

    public void Level2MecanismData(int value)
    {
        PlayerPrefs.SetInt("level2Mecanism", value);
    }

    public void Level4MecanismData(int value)
    {
        PlayerPrefs.SetInt("level4Mecanism", value);
    }
}
