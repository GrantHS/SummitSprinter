using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerDataSO playerDataSO;
    private PlayerMovement _playerMovement;
    private BadGuy _badGuy;
    private GameObject _roof;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    private float minYpos = -20;
    public float respawnTime = 3f;

    private void Awake()
    {
        if (!GetComponent<PlayerMovement>())
        {
            gameObject.AddComponent<PlayerMovement>();
        }
        
        _playerMovement = GetComponent<PlayerMovement>();

        _roof = FindChildWithTag(this.gameObject, "Cargo");
   
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
            StartCoroutine(GameManager.Instance.Respawn(this.gameObject, respawnTime, spawnPos, spawnRot));
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
            StartCoroutine(GameManager.Instance.Respawn(this.gameObject, respawnTime, spawnPos, spawnRot));
        }
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
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
