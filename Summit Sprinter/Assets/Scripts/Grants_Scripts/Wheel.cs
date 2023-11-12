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
    public Vector3 spawnPos;
    public Quaternion spawnRot;

    public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }
    public GameObject Player { get => player; private set => player = value; }

    private void Awake()
    {
        Player = GetComponent<ConfigurableJoint>().connectedBody.gameObject;
        spawnPos = transform.position;
        //Debug.Log("Player = " + Player.name);
    }

    public void Respawn()
    {
        GameManager.Instance.InvokeDeath(this.gameObject, 3f, spawnPos, spawnRot);
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
