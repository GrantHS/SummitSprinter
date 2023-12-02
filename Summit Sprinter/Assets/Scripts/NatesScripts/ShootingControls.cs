 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingControls : MonoBehaviour
{
    private PlayerDataSO playerDataSO;
    public int coinsToAdd = 100;

  

    public void DeactivateAndAddScore()
    {
        gameObject.SetActive(false);

      playerDataSO.numCoins += coinsToAdd;

        // You can print the overall score to the console for testing
        Debug.Log("Overall Score: " + playerDataSO.numCoins);
    }
}
