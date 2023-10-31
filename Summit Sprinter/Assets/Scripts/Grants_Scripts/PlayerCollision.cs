using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public PlayerDataSO playerDataSO;
    private PlayerMovement _playerMovement;
    private BadGuy _badGuy;
    public GameObject mainUI;
    public GameObject endUI;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    private float minYpos = -50;
    public float respawnTime = 3f;
    public Text coinCount;
    public Text skrapCount;


    //Merge Items
    public bool Merge_1On;
    public bool Merge_2On;
    public bool Merge_3On;

    public GameObject Merge_1;
    public GameObject Merge_2;
    public GameObject Merge_3;



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
            GameManager.Instance.InvokeDeath(this.gameObject, respawnTime, spawnPos, spawnRot);
        }

        //Merge Update
        if (Merge_1On)
        {
            Merge_1.gameObject.SetActive(true);
        }
        else
        {
            Merge_1.gameObject.SetActive(false);
        }


        if (Merge_2On)
        {
            Merge_2.gameObject.SetActive(true);
        }
        else
        {
            Merge_2.gameObject.SetActive(false);
        }


        if (Merge_3On)
        {
            Merge_3.gameObject.SetActive(true);
        }
        else
        {
            Merge_3.gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            playerDataSO.numCoins++;
            coinCount.text = ": " + playerDataSO.numCoins.ToString();
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
            skrapCount.text = "Skraps: " + playerDataSO.numSkrap.ToString();
            //Debug.Log("You have " + numSkrap + " skrap");
        }




        // for merge items 

        if (other.gameObject.CompareTag("Merge 1"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Merged");
            Merge_1On = true;
        }

        if (other.gameObject.CompareTag("Merge 2"))
        {
            other.gameObject.SetActive(false);
            Merge_2On = true;
        }


        if (other.gameObject.CompareTag("Merge 3"))
        {
            other.gameObject.SetActive(false);
            Merge_3On = true;
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            GameObject go = collision.gameObject;
            Debug.Log("colliding with " + go.name);

            StartCoroutine(FlyAway(go, 5f));

            mainUI.SetActive(false);
            endUI.SetActive(true);
            endUI.GetComponent<PauseMenu>().isPaused = true;
        }
    }

    private IEnumerator FlyAway(GameObject go, float lifeTime)
    {
        if (!go.GetComponent<Rigidbody>())
        {
            go.AddComponent<Rigidbody>();
        }

        Rigidbody rb = go.GetComponent<Rigidbody>();
        Vector3 angleDirection = new Vector3(1, 1, 0);
        float flySpeed = 50f;
        rb.AddForce(angleDirection * flySpeed);
        yield return new WaitForSeconds(lifeTime);
        go.SetActive(false);
        
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
