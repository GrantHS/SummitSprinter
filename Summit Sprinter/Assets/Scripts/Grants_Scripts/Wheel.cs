using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public bool isGrounded;
    private GameObject player;
    private float respawnTime = 3f;
    private EnemyMovement enemyMovement;

    public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }
    public GameObject Player { get => player; private set => player = value; }

    private void Awake()
    {
        Player = GetComponent<ConfigurableJoint>().connectedBody.gameObject;
        //Debug.Log("Player = " + Player.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spikey"))
        {
            //enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();
            //enemyMovement.SpikeyAttack();
            //EnemyRespawn(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}
