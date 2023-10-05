using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBadGuySO", menuName = "Scriptable Objects/BadGuySO")]
public class BadGuy : ScriptableObject
{
    public Enemies enemyType;

    public float damage;
    public float health;
    public float speed;

}
