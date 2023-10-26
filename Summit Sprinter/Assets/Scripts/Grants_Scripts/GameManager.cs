using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    private PlayerMovement playerMovement;
    public GameObject playerObject;
    public Canvas deathCanvas;
    private float idleDrain = 0.5f;
    public GameObject levelUI;
    public GameObject startUI;

    public bool isDead
    {
        get; private set;
    }

    public float gasLevel;


    public void InvokeDeath(GameObject player, float respawnTime, Vector3 spawnPos, Quaternion spawnRot)
    {
        StartCoroutine(Respawn(player, respawnTime, spawnPos, spawnRot));
    }

    public void Restart()
    {
      
    }
    private IEnumerator Respawn(GameObject player, float respawnTime, Vector3 spawnPos, Quaternion spawnRot)
    {
        isDead = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        MeshRenderer mr = player.GetComponent<MeshRenderer>();
        float blinkTime = respawnTime/5;

  
        playerMovement = player.GetComponent<PlayerMovement>();
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        mr.enabled = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();
        playerMovement._currentVelocity = 0;

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

    private void Start()
    {
        Debug.Log("Game Manager Active");
        levelUI.SetActive(false);
        startUI.SetActive(true);
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    public void OnStartPressed()
    {
        StartGame();
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

  
}
