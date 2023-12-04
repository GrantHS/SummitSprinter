using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


//Some functions moved from MenuLogic.cs for organization
public class GameManager : Singleton<GameManager>
{
    private PlayerMovement playerMovement;
    public GameObject playerObject;
    public GameObject _playerVehicle;
    public Canvas deathCanvas;
    private float idleDrain = 0.5f;
    public GameObject levelUI;
    public GameObject startUI;
    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject creditsUI;
    public GameObject mainCam;
    public Vector3 playerStartPos;

    public bool isDead
    {
        get; private set;
    }

    public float gasLevel;
    private void Init()
    {
        if (_playerVehicle == null)
        {
            _playerVehicle = playerObject.GetComponentInChildren<PlayerMovement>().gameObject;
        }

        if (playerMovement == null)
        {
            playerMovement = _playerVehicle.GetComponent<PlayerMovement>();
        }
        playerMovement.gasMeter.FillTank();
        Debug.Log("Game Manager Active");
        levelUI.SetActive(true);
        startUI.SetActive(false);
        creditsUI.SetActive(false);
       // winUI.SetActive(false);
        mainCam.SetActive(true);
        playerObject.SetActive(true);
    }

    private void Start()
    {
        if (_playerVehicle == null)
        {
            _playerVehicle = playerObject.GetComponentInChildren<PlayerMovement>().gameObject;
        }

        if (playerMovement == null)
        {
            playerMovement = _playerVehicle.GetComponent<PlayerMovement>();
        }
        playerMovement.gasMeter.FillTank();
        Debug.Log("Game Manager Active");
        levelUI.SetActive(false);
        startUI.SetActive(true);
        creditsUI.SetActive(false);
        //winUI.SetActive(false);
        mainCam.SetActive(true);
        playerObject.SetActive(true);

        

        
    }

    public void InvokeDeath(GameObject player, float respawnTime, Vector3 spawnPos, Quaternion spawnRot)
    {
        StartCoroutine(Respawn(player, respawnTime, spawnPos, spawnRot));
    }

    public void OnCreditsPressed()
    {
        levelUI.SetActive(false);
        startUI.SetActive(true);
        creditsUI.SetActive(true);
        winUI.SetActive(false);
        //mainCam.SetActive(true);
        //playerObject.SetActive(false);
        pauseUI.SetActive(false);

    }

    //Restart
    public void MainMenu()
    {
        Time.timeScale = 1f;
        levelUI.SetActive(false);
        startUI.SetActive(true);
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        creditsUI.SetActive(false);
        Cursor.visible = true;

        playerMovement.ButtonRelease();

        _playerVehicle.GetComponent<PlayerCollision>().ResetPosition();
        //_playerVehicle.transform.position = playerStartPos;

        _playerVehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;

        foreach (Wheel wheel in playerMovement.wheels)
        {
            wheel.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        /*
        _playerVehicle.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Wheel wheel in playerMovement.wheels)
        {
            wheel.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _playerVehicle.GetComponent<Rigidbody>().isKinematic = true;
            //whe
            //whe
        }
        playerObject.transform.position = playerStartPos;
        _playerVehicle.GetComponent<Rigidbody>().isKinematic = false;
        foreach (Wheel wheel in playerMovement.wheels)
        {
            _playerVehicle.GetComponent<Rigidbody>().isKinematic = false;
        }
        */







        //Scene currentScene = SceneManager.GetActiveScene();
        // Reload the current scene
        //SceneManager.LoadScene(currentScene.buildIndex);
        //Start();
    }
    private IEnumerator Respawn(GameObject player, float respawnTime, Vector3 spawnPos, Quaternion spawnRot)
    {
        isDead = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        MeshRenderer mr = player.GetComponent<MeshRenderer>();
        float blinkTime = respawnTime/10;

        //this.gameObject.GetComponent<Renderer>().enabled = false;
        mr.enabled = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();

        player.transform.position = spawnPos;
        player.transform.rotation = spawnRot;
        
        yield return new WaitForSeconds(blinkTime);
        mr.enabled = true;
        yield return new WaitForSeconds(blinkTime);
        mr.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        mr.enabled = true;
        yield return new WaitForSeconds(blinkTime);
        mr.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        rb.isKinematic = false;
        mr.enabled = true;
        isDead = false;
        //this.gameObject.GetComponent<Renderer>().enabled = true;


    }

    private void OnEnable()
    {
       
    }

    

    public void OnStartPressed()
    {
       Init();
    }

    private void StartGame()
    {
        startUI.SetActive(false);
        //StartCoroutine(playerMovement.StartingMove());
        levelUI.SetActive(true);

    }

    public void OnQuitPressed()
    {
        QuitGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void OnResumePressed()
    {
        Resume();
    }

    //also from MenuLogic.cs
    private void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    //MenuLogic.cs
    public void Pause()
    {

        Debug.Log("paused");
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        // this.gameObject.SetActive(true);
        


    }

    public void OnGameWin()
    {
        Debug.Log("Game Won!");
        //Time.timeScale = 0f;
        // this.gameObject.SetActive(true);
        playerObject.SetActive(false);
        levelUI.SetActive(false);
        mainCam.SetActive(false);
        winUI.SetActive(true);
    }

}
