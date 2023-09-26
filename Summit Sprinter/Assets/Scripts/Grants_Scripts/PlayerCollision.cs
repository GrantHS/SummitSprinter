using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerDataSO playerDataSO;

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


    }
}
