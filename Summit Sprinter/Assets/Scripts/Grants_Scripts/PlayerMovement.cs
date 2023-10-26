using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GasMeter gasMeter;
    public Wheel[] wheels = new Wheel[4];
    private float _powerDrain = 2f;
    //public PlayerDataSO playerData;
    public float _currentVelocity = 0f;
    private float _topSpeed =10f;
    private float _torque = 25f;
    private float _acceleration = 20f;
    //private float _wheelSpeed = 40f;
    //private float _rotateSpeed = 10f;
    private InputAction _move;
    private Vector2 _moveDirection;

    public PlayerInputActions playerControls;
    private SuspensionSystem _suspensionSystem;
    private Rigidbody rb;

    public bool isGrounded = true;
    private int groundedWheels = 0;
    private bool _goingForward = false;
    private bool _goingBackwards = false;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        _suspensionSystem = GetComponent<SuspensionSystem>();
        //DontDestroyOnLoad(this.gameObject);
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
        foreach (Wheel wheel in wheels)
        {
            if (wheel.isGrounded)
            {
                groundedWheels++;
            }

            
        }

        if (groundedWheels >= 4)
        {
            isGrounded = true;
        }
        else isGrounded = false;

        //Debug.Log("Current Speed: " + _currentVelocity);
        if (gasMeter.currentValue > 0)
        {
            if (_goingForward)
            {
                rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
                gasMeter.currentValue -= _powerDrain * Time.deltaTime;

                if (_currentVelocity < _topSpeed)
                {
                    _currentVelocity += _acceleration * Time.deltaTime;
                }

                if (!isGrounded) rb.AddTorque(Vector3.forward * -_torque, ForceMode.Force);
            }
            else if (_goingBackwards)
            {
                rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
                gasMeter.currentValue -= _powerDrain * Time.deltaTime;

                if (_currentVelocity < _topSpeed)
                {
                    _currentVelocity -= _acceleration * Time.deltaTime;
                }

               if (!isGrounded) rb.AddTorque(Vector3.forward * _torque, ForceMode.Force);

            }
            else
            {
                if (_currentVelocity > 0)
                {
                    _currentVelocity -= _acceleration * Time.deltaTime;
                }
                else if (_currentVelocity < 0)
                {
                    _currentVelocity += _acceleration * Time.deltaTime;
                }
            }
        }
        else
        {
            Debug.Log("Out of gas");
            if (_currentVelocity > 0)
            {
                _currentVelocity -= _acceleration * Time.deltaTime;
            }
            else if (_currentVelocity < 0)
            {
                _currentVelocity += _acceleration * Time.deltaTime;
            }
        }

        //Keep on bottom of method
        groundedWheels = 0;


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


    public IEnumerator StartingMove()
    {
        _goingForward = true;
        yield return new WaitForSeconds(2.0f);
        _goingForward = false;
    }
}
