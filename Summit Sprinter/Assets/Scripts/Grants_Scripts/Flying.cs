using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody[] rbs;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    private Wheel[] wheels;
    public bool canFly = true;
    private float airTorque = 20f;

    public float AirTorque { get => airTorque; private set => airTorque = value; }

    private void Start()
    {
        canFly = true;
        playerMovement = GetComponent<PlayerMovement>();
        _player = GameManager.Instance.playerObject;
        rb = GetComponent<Rigidbody>();
        rbs = _player.GetComponentsInChildren<Rigidbody>();
        wheels = playerMovement.wheels;
    }

    private void Update()
    {
        if (canFly)
        {
            rb.useGravity = false;
            foreach (var wheel in wheels)
            {
                if (!wheel.isGrounded)
                {
                    wheel.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        else
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.useGravity = true;
            }
        }
    }
}
