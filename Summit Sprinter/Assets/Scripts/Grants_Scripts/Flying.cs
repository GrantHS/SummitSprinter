using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Flying : MonoBehaviour
{
    private GameObject _player;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    private Wheel[] wheels;
    public bool hasWings = false;
    public bool _isFlying = false;
    private float airTorque = 20f;
    private float _velocity = 0f;
    private float _topSpeed = 5f;
    private float _acceleration = 1f;
    private float shutOffTime = 2f;

    public float AirTorque { get => airTorque; private set => airTorque = value; }

    private void OnEnable()
    {
        //temp setting
        hasWings = true;
        playerMovement = GetComponent<PlayerMovement>();
        _player = GameManager.Instance.playerObject;
        rb = GetComponent<Rigidbody>();
        wheels = playerMovement.wheels;
    }

    private void OnDisable()
    {
        hasWings = false;
        foreach (var wheel in wheels)
        {
            wheel.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void Update()
    {
        if (playerMovement.isGrounded)
        {
            _isFlying = false;
        }
        else if(playerMovement.GroundedWheels == 0) _isFlying = true;
        

        if (hasWings && _isFlying)
        {
            rb.useGravity = false;
            foreach (var wheel in wheels)
            {
                if (!wheel.isGrounded)
                {
                    wheel.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    _isFlying = false;
                    break;
                }
            }

            if (_isFlying) Accelerate();
            
        }
        else if (hasWings && !_isFlying )
        {
            CutEngine();
        }
    }

    public void CutEngine()
    {
        StartCoroutine(StopEngine());
    }

    private IEnumerator StopEngine()
    {
        Debug.Log("Stopping Engine");
        hasWings = false;
        GravityControl(true);
        yield return new WaitForSeconds(shutOffTime);
        hasWings = true;
    }

    private void Accelerate()
    {
        //rb.AddForce(Vector3.left * _velocity, ForceMode.Force);
        rb.velocity += Vector3.left * _velocity * Time.deltaTime;

        if (_velocity < _topSpeed)
        {
            _velocity += _acceleration * Time.deltaTime;
        }
    }

    public void ChangeGravity(bool usingGravity)
    {
        GravityControl(usingGravity);
    }

    private void GravityControl(bool usingGravity)
    {
        if (usingGravity)
        {
            foreach (var wheel in wheels)
            {
                wheel.GetComponent<Rigidbody>().useGravity = true;
            }
            rb.useGravity = true;
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.GetComponent<Rigidbody>().useGravity = false;
            }
            rb.useGravity = false;
        }
    }
}