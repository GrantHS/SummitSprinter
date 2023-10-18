using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftSpot : MonoBehaviour
{
    private PlayerCollision playerCollision;
    public GameObject deathCanvas;
    public Slider GasSlider;
    private void Awake()
    {
        playerCollision = GetComponentInParent<PlayerCollision>();
       
        deathCanvas.SetActive(false);
    }

  
    private void Update()
    {
        if (GasSlider.value < 0.01f)
        {
            Debug.Log("is zero");

            deathCanvas.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
          //  deathCanvas.GetComponent<deathScript>().Death();
            deathCanvas.SetActive(true);
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
