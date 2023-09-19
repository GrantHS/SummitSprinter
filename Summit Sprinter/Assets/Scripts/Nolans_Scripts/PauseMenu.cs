using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuCanvas;
    public GameObject endMenuCanvas;
    public GameObject youDiedCanvas;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
    }


    void Update()
    {
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
    }

    public void Pause()
    {
        if (!endMenuCanvas.activeSelf && !youDiedCanvas.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenuCanvas.SetActive(true);
            isPaused = true;

            //Disable the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        isPaused = false;

        //Enable the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
