using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScript : MonoBehaviour
{
    public bool dead = false;
    public GameObject DeathCanvas;
    //public GameObject endMenuCanvas;



    private void Awake()
    {

    }


    void Update()
    {
        if (dead)
        {
            DeathCanvas.SetActive(true);
        }
        else
        {
            DeathCanvas.SetActive(false);
        }
    }

    public void Death()
    {
        dead = true;
        Debug.Log("paused");
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
        DeathCanvas.SetActive(true);        
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

  
}
