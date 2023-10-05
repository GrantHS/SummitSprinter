using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerDataSO playerDataSO;
    private PlayerMovement _playerMovement;
    private BadGuy _badGuy;
    private GameObject _roof;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    private float minYpos = -50;
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
            GameManager.Instance.InvokeDeath(this.gameObject, respawnTime, spawnPos, spawnRot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            playerDataSO.numCoins++;
            //Debug.Log("You have " + numCoins + " coins");
        }

        if (other.gameObject.CompareTag("Gas"))
        {
            other.gameObject.SetActive(false);
            _playerMovement.gasMeter.FillTank();
            //Debug.Log("Your tank is now full");
        }

        if (other.gameObject.CompareTag("End"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("You win!");
        }

        if (other.gameObject.CompareTag("Skrap"))
        {
            other.gameObject.SetActive(false);
            playerDataSO.numSkrap++;
            //Debug.Log("You have " + numSkrap + " skrap");
        }

        if (other.GetComponent<EnemyMovement>())
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

    public void TakeDamage(BadGuy badGuy)
    {
        _playerMovement.gasMeter.currentValue -= badGuy.damage;
        /*
        if(playerDataSO.playerHealth <= 0)
        {
            GameManager.Instance.InvokeDeath(this.gameObject, respawnTime, spawnPos, spawnRot);
        }
        */

        switch (badGuy.enemyType)
        {
            case Enemies.Spikey:
                //badGuy.GetComponent<BoxCollider>().isTrigger = true;
                break;
            case Enemies.Wheelie:
                
                break;
            case Enemies.Rocketee:
                
                break;
            default:
                break;
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
