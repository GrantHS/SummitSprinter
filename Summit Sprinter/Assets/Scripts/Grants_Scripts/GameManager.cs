using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public PlayerMovement playerMovement;
    public IEnumerator Respawn(PlayerMovement player, float respawnTime, Vector3 spawnPos)
    {
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        player.gameObject.SetActive(false);
        player.gameObject.transform.position = spawnPos;
        playerMovement._currentVelocity = 0;
        yield return new WaitForSeconds(respawnTime);
        player.gameObject.SetActive(true);
        //this.gameObject.GetComponent<Renderer>().enabled = true;

    }

    private void Start()
    {
        Debug.Log("Game Manager Active");
    }
}
