using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScriptableObject", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int numCoins;
    public int totalValue;

    public float playerHealth;
    public float playerSpeed;
    public int playerScore;
    public int highScore;


}
