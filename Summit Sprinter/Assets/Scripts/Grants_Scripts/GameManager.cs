using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public PlayerMovement playerMovement;
    public GameObject playerPrefab;

    public bool isDead
    {
        get; private set;
    }
    public IEnumerator Respawn(PlayerMovement player, float respawnTime, Vector3 spawnPos, Quaternion spawnRot)
    {
        isDead = true;

        Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
        MeshRenderer mr = player.gameObject.GetComponent<MeshRenderer>();
        float blinkTime = respawnTime/5;

  
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        mr.enabled = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();
        playerMovement._currentVelocity = 0;

        player.gameObject.transform.position = spawnPos;
        player.gameObject.transform.rotation = spawnRot;
        
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

    private void Start()
    {
        Debug.Log("Game Manager Active");
    }
}
