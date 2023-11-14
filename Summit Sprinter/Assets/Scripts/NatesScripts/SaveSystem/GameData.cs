using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int CoinsCount;
    public float MouseSens;

    // the values defined in this constructor will be default values 
    //the game starts with when theres no data to load 

    public GameData()
    {
        this.CoinsCount = 0;
        this.MouseSens = 0;
    }


}