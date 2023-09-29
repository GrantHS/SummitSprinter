using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerDataSO playerDataSO;
    private PlayerMovement _playerMovement;
    private BadGuy _badGuy;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private float minYpos = -20;
    private float respawnTime = 3f;

    private void Awake()
    {
        if (!GetComponent<PlayerMovement>())
        {
            gameObject.AddComponent<PlayerMovement>();
        }
        
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }

    private void Update()
    {
        if(transform.position.y <= minYpos)
        {
            StartCoroutine(GameManager.Instance.Respawn(_playerMovement, respawnTime, spawnPos, spawnRot));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            playerDataSO.numCoins++;
           // Debug.Log("You have " + numCoins + " coins");
        }

        if (other.gameObject.CompareTag("Gas"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Your tank is now full");
        }

        if (other.gameObject.CompareTag("End"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("You win!");
        }

        if (other.GetComponent<BadGuy>())
        {
            _badGuy = other.GetComponent<BadGuy>();
            switch (_badGuy.enemyType)
            {
                case Enemies.Spikey:
                    TakeDamage(_badGuy);
                    break;
                case Enemies.Wheelie:
                    TakeDamage(_badGuy);
                    break;
                case Enemies.Rocketee:
                    TakeDamage(_badGuy);
                    break;
                default:
                    break;
            }
        }


    }
    
    private void TakeDamage(BadGuy badGuy)
    {
        playerDataSO.playerHealth -= badGuy._damage;

        if(playerDataSO.playerHealth <= 0)
        {
            StartCoroutine(GameManager.Instance.Respawn(_playerMovement, respawnTime, spawnPos, spawnRot));
        }
    }

    /*
    private IEnumerator Respawn(float respawnTime)
    {
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(false);
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        _playerMovement._currentVelocity = 0;
        yield return new WaitForSeconds(respawnTime);
        gameObject.SetActive(true);
        //this.gameObject.GetComponent<Renderer>().enabled = true;

    }
    */
}
