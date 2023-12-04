using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScriptableObject", menuName = "Scriptable Objects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int numCoins;
    public int numSkrap;
    public int totalValue;

    public float playerHealth;
    public float playerSpeed;
    public int playerScore;
    public int highScore;
    


}
