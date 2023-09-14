using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _playerSpeed = 25f;
    private float _wheelSpeed = 40f;
    private float _rotateSpeed = 10f;
    private InputAction _move;
    private Vector2 _moveDirection = Vector2.zero;
    public Wheel[] _wheels = new Wheel[4];

    public PlayerInputActions playerControls;
    private Rigidbody rb;

    public float minChassisHeight;
    public float suspensionStrength;
    private bool _isGrounded;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        _wheels = GetComponentsInChildren<Wheel>();
    }

    private void OnEnable()
    {
        _move = playerControls.PlayerMovement.Move;
        _move.Enable();
        rb = this.GetComponent<Rigidbody>();

        //_reverse = playerControls.PlayerMovement.Reverse;
    }

    private void OnDisable()
    {
        _move.Disable();
        //_reverse.Disable(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        ActiveSuspension();

        //Accelerate
        if (_isGrounded)
        {
            rb.AddForce(_moveDirection * _playerSpeed, ForceMode.Force);
        }
        
        //Rotate wheels
        foreach (Wheel wheel in _wheels)
        {
            rb.AddTorque(_moveDirection * _wheelSpeed, ForceMode.Force);
        }

        //Spin vehicle
        rb.AddTorque(_moveDirection * _playerSpeed, ForceMode.Force);
    }



    // Update is called once per frame
    void Update()
    {
        _moveDirection = -_move.ReadValue<Vector2>();
        Debug.Log(_moveDirection);
    }

    private void ActiveSuspension()
    {
        float maxWheelHeight = 0.0f;
        int groundedWheels =0;
        Vector3 chassisPosition = this.transform.position;
        chassisPosition.y -= this.transform.localScale.y / 2;

        foreach (Wheel wheel in _wheels)
        {
            if (wheel.isGrounded)
            {
                groundedWheels++;
                Debug.Log(groundedWheels + " wheels on the ground");
            }

            if(groundedWheels >= 1) _isGrounded = true;
        }

        if (_isGrounded)
        {
            int i = 0;
            foreach (Wheel wheel in _wheels)
            {
                i++;
                Debug.Log("Wheel " + i + "'s Y position: " + wheel.transform.position.y);
                if (wheel.transform.position.y > maxWheelHeight)
                {
                    maxWheelHeight = wheel.transform.position.y;
                }
            }
            Debug.Log("Max wheel height: " + maxWheelHeight);

            if(chassisPosition.y < maxWheelHeight + minChassisHeight)
            {
                chassisPosition.y += suspensionStrength;
                transform.position = chassisPosition;
                if (chassisPosition.y >= maxWheelHeight + minChassisHeight)
                {
                    chassisPosition.y = maxWheelHeight + minChassisHeight;
                    transform.position = chassisPosition;
                }
            }
        }
        Debug.Log(chassisPosition);
    }
}
