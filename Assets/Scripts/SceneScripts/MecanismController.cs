using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MecanismController : MonoBehaviour
{
    public int numberOfLevers;
    public bool hasToActivate, hasToDesactivate;
    public int leversActive;
    public GameObject objectToActivate, objectToDesactivate;

    private void Start()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                if (PlayerPrefs.GetInt("level1Mecanism") == 1)
                {
                    objectToActivate.SetActive(true);
                    objectToDesactivate.SetActive(false);
                }
                break;
            case 2:
                if(PlayerPrefs.GetInt("level2Mecanism") == 1)
                {
                    objectToDesactivate.SetActive(false);
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("level4Mecanism") == 1)
                {
                    objectToDesactivate.SetActive(false);
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(leversActive == numberOfLevers)
        {
            if (hasToActivate)
            {
                objectToActivate.SetActive(true);
            }

            if (hasToDesactivate)
            {
                objectToDesactivate.SetActive(false);
            }

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    DataManager.instance.Level1MecanismData(1);
                    break;
                case 2:
                    DataManager.instance.Level2MecanismData(1);
                    break;
                case 4:
                    DataManager.instance.Level4MecanismData(1);
                    break;
            }
        }
    }
}
