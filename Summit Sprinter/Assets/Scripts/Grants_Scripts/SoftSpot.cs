using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftSpot : MonoBehaviour
{
    private PlayerCollision playerCollision;
    public GameObject deathCanvas;
    private void Awake()
    {
        playerCollision = GetComponentInParent<PlayerCollision>();
        deathCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            deathCanvas.GetComponent<deathScript>().Death();
            //deathCanvas.SetActive(true);
            //deathCanvas.GetComponent<PauseMenu>().isPaused = true;
            //Time.timeScale = 0f;
            
            // GameManager.Instance.InvokeDeath(gameObject.GetComponentInParent<PlayerMovement>().gameObject, playerCollision.respawnTime, playerCollision.spawnPos, playerCollision.spawnRot);

            if (GetComponentInParent<PlayerMovement>() == null)
            {
                Debug.Log("null player");

            }
            
        }
    }


    public void Restart()
    {
        GameManager.Instance.InvokeDeath(gameObject.GetComponentInParent<PlayerMovement>().gameObject, playerCollision.respawnTime, playerCollision.spawnPos, playerCollision.spawnRot);
        Time.timeScale = 1f;
        deathCanvas.SetActive(false);

    }
}
