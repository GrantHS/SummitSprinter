using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuCanvas;
    public GameObject player;
    //public GameObject endMenuCanvas;

   

    private void Awake()
    {

        TouchSimulation.Enable();
    }


    void Update()
    {
        /*
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
        }
        */
    }

    public void Pause()
    {   
        isPaused = true;
        Debug.Log("paused");
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
        pauseMenuCanvas.SetActive(true);
       

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        this.gameObject.SetActive(false);
        isPaused = false;
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
      //  GameManager.Instance.startUI.SetActive(false);
        //player.GetComponent<SoftSpot>().Restart();
        Time.timeScale = 1f;
        Cursor.visible = true;

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


}