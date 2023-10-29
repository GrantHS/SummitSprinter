using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject HowToPlayCanvas;
    public GameObject UpgradesCanvas;
    public GameObject MainMenuCanvas;
    public GameObject LevelUICanvas;

    public GameObject pauseMenuCanvas;
    public GameObject player;



    private void Awake()
    {
        LevelUICanvas.SetActive(false);
    }
    public void HowToPlay()
    {
        HowToPlayCanvas.SetActive(true);
        UpgradesCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
    }

    public void UpgradesButton()
    {
        UpgradesCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(false);

    }

    public void BackButton()
    {
        MainMenuCanvas.SetActive(true);
        UpgradesCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }

    public void Starting()
    {   
        LevelUICanvas.SetActive(true);
        UpgradesCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
       
    }


    public void Pause()
    {
       
        Debug.Log("paused");
        Time.timeScale = 0f;
       // this.gameObject.SetActive(true);
        pauseMenuCanvas.SetActive(true);


    }


    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
       // this.gameObject.SetActive(false);
       
    }

    public void Restart()
    {
        //Scene scene = SceneManager.GetActiveScene();
       // SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        LevelUICanvas.SetActive(false);
        Cursor.visible = true;
        // MainMenuCanvas.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.buildIndex);

    }



    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}

