using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _playerSpeed = 25f;
    //private float _wheelSpeed = 40f;
    //private float _rotateSpeed = 10f;
    private InputAction _move;
    private Vector2 _moveDirection;

    public PlayerInputActions playerControls;
    private Rigidbody rb;

    private bool _isGrounded;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
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
        //Vector3.Clamp
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveDirection = _move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {

        rb.AddForce(_moveDirection * _playerSpeed, ForceMode.Force);

        /*
        //Accelerate
        if (_isGrounded)
        {
            
        }
        
        //Rotate wheels
        foreach (Wheel wheel in _wheels)
        {
            rb.AddTorque(_moveDirection * _wheelSpeed, ForceMode.Force);
        }

        //Spin vehicle
        rb.AddTorque(_moveDirection * _playerSpeed, ForceMode.Force);
        */
    }



    // Update is called once per frame
    void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
        Debug.Log(_moveDirection);
        if (!_move.enabled) Debug.Log("Null action list");
    }
}
