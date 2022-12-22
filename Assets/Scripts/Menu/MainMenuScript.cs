using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "MainMenu")
        {
            AudioManager.instance.BGMusic.Stop();
            AudioManager.instance.PlayAudio(AudioManager.instance.BGMusic);
        }

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Game closed");
    }

    public void GoToMainMenu()
    {
        print("Funciona");
        AudioManager.instance.BGMusic.Stop();
        
        SceneManager.LoadScene(0);
    }

    public void ShowSettings()
    {
        anim.SetBool("ShowSettings", true);
    }

    public void HideSettings()
    {
        anim.SetBool("ShowSettings", false);
    }
}
