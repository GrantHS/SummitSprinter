using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //public PlayerDataSO playerData;
    public float _currentVelocity = 0f;
    private float _topSpeed =10f;
    private float _torque = 5f;
    private float _acceleration = 3f;
    //private float _wheelSpeed = 40f;
    //private float _rotateSpeed = 10f;
    private InputAction _move;
    private Vector2 _moveDirection;

    public PlayerInputActions playerControls;
    private Rigidbody rb;

    public bool isGrounded = true;
    private bool _goingForward = false;
    private bool _goingBackwards = false;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        
    }

    private void OnEnable()
    {
       // _move = playerControls.PlayerMovement.NewMove;
        //_move.Enable();
        rb = this.GetComponent<Rigidbody>();

        //_reverse = playerControls.PlayerMovement.Reverse;
    }

    private void OnDisable()
    {
       // _move.Disable();
        //_reverse.Disable(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        //Vector3.Clamp
    }

    public void Move(InputAction.CallbackContext context)
    {
       // _moveDirection = _move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //Debug.Log("Current Speed: " + _currentVelocity);
        if (_goingForward)
        {
            
            rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
            rb.AddTorque(Vector3.forward * -_torque, ForceMode.Force);

            if(_currentVelocity < _topSpeed)
            {
                _currentVelocity += _acceleration * Time.deltaTime;
            }
            
        }
        else if(_goingBackwards)
        {
            rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
            rb.AddTorque(Vector3.forward * _torque, ForceMode.Force);

            if (_currentVelocity < _topSpeed)
            {
                _currentVelocity -= _acceleration * Time.deltaTime;
            }
        }
        else
        {
            if(_currentVelocity > 0 )
            {
                _currentVelocity -= _acceleration * Time.deltaTime;
            }
            else if( _currentVelocity < 0 )
            {
                _currentVelocity += _acceleration * Time.deltaTime;
            }
        }
        

    }

    public void Forward()
    {
        //Debug.Log("Forward");
        //_currentSpeed = _startSpeed;
        _goingBackwards = false;
        _goingForward = true;

        /*
        rb.AddForce(Vector2.right * _playerSpeed, ForceMode.Force);
        if (Vector2.right == null)
        {
            Debug.Log("null Vector");
            //rb.AddForce(Vector2.right * _playerSpeed, ForceMode.Force);
        }
        Debug.Log("Grounded: " + isGrounded);
        

        _playerSpeed = 25f;
        */
    }

    public void ButtonRelease()
    {
        //Debug.Log("Released");
        _goingForward = false;
        _goingBackwards = false;
        _currentVelocity -= _acceleration * Time.deltaTime;
    }

    public void Reverse()
    {
        //Debug.Log("Reverse");
        //_currentSpeed = _startSpeed;
        _goingForward = false;
        _goingBackwards = true;
        /*
        rb.AddForce(Vector2.left * _playerSpeed, ForceMode.Force);
        if (isGrounded)
        {
            Debug.Log("Reverse");
            //rb.AddForce(Vector2.left * _playerSpeed, ForceMode.Force);
        }
        Debug.Log("Grounded: " + isGrounded);
        

        _playerSpeed = -25f;
        */
    }



    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.left * _currentVelocity, ForceMode.Force);

    }
}
