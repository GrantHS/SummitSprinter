using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuCanvas;
    //public GameObject endMenuCanvas;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        TouchSimulation.Enable();
    }


    void Update()
    {
       /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
       */
    }

    public void Pause()
    {
       Debug.Log("paused"); 
       Time.timeScale = 0f;
       pauseMenuCanvas.SetActive(true);
       isPaused = true;
        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        isPaused = false;
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
        Cursor.visible = true;

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


}
