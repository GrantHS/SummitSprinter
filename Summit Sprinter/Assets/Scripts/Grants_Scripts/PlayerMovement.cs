using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GasMeter gasMeter;
    private Flying _flying;
    public Wheel[] wheels = new Wheel[4];
    private float _powerDrain = 2f;
    public float _currentVelocity = 0f;
    private float _topSpeed =10f;
    private float _torque = 50f;
    private float _acceleration = 20f;

    public PlayerInputActions playerControls;
    private Rigidbody rb;

    public bool isGrounded = true;
    private int groundedWheels;
    private bool _goingForward = false;
    private bool _goingBackwards = false;

    public bool GoingForward { get => _goingForward; private set => _goingForward = value; }
    public int GroundedWheels { get => groundedWheels; private set => groundedWheels = value; }

    private void Awake()
    {
        
        groundedWheels = 0;
        playerControls = new PlayerInputActions();
        _flying = this.gameObject.GetComponent<Flying>();
        _flying.enabled = false;
        Debug.Log("Awake!");
        //DontDestroyOnLoad(this.gameObject);
    }

    

    private void OnEnable()
    {
       // _move = playerControls.PlayerMovement.NewMove;
        //_move.Enable();
        rb = this.GetComponent<Rigidbody>();

        //_reverse = playerControls.PlayerMovement.Reverse;
    }

    private void FixedUpdate()
    {

        Forward();
        foreach (Wheel wheel in wheels)
        {
            //Debug.Log(wheel.name + " is grounded = " + wheel.IsGrounded);
            if (wheel.IsGrounded)
            {
                groundedWheels++;

                if (groundedWheels >= 4)
                {
                    groundedWheels = 4;
                    isGrounded = true;
                }
                else isGrounded = false;
            }
            else if (!wheel.IsGrounded)
            {
                if(groundedWheels > 0)
                {
                    groundedWheels--;
                }
                else if (groundedWheels <= 0)
                {
                    groundedWheels = 0;
                }
            }

            
        }



        //Debug.Log("Grounded Wheels: " + groundedWheels);


        //Debug.Log("Current Speed: " + _currentVelocity);
        if (gasMeter.currentValue > 0)
        {
            
            if (GoingForward)
            {
                if (isGrounded)
                {
                    //Debug.Log("moving");
                    rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
                    gasMeter.currentValue -= _powerDrain * Time.deltaTime;

                    if (_currentVelocity < _topSpeed)
                    {
                        _currentVelocity += _acceleration * Time.deltaTime;
                    }
                    rb.AddTorque(Vector3.forward * -_torque, ForceMode.Force);
                }
                else if (_flying._isFlying)
                {
                    _flying.FlightControl(false);
                
                    //rb.AddTorque(Vector3.forward * _flying.AirTorque, ForceMode.Force);
                    //rb.AddRelativeTorque(Vector3.forward * _flying.AirTorque, ForceMode.Force);
                    /*
                    transform.Rotate(Vector3.forward * -_flying.AirTorque * Time.deltaTime);
                    if (transform.rotation.z !<= maxZRot.z)
                    {
                        transform.rotation = Quaternion.LookRotation(maxZRot);
                    }
                    */
                }
            }
            else if (_goingBackwards)
            {
                if (isGrounded)
                {
                    //Debug.Log("moving");
                    rb.AddForce(Vector3.left * _currentVelocity, ForceMode.Force);
                    gasMeter.currentValue -= _powerDrain * Time.deltaTime;

                    if (_currentVelocity < _topSpeed)
                    {
                        _currentVelocity -= _acceleration * Time.deltaTime;
                    }
                    rb.AddTorque(Vector3.forward * _torque, ForceMode.Force);
                }                               
                
                

            }
            else
            {
                //Debug.Log("not moving");
                if (_currentVelocity > 0)
                {
                    _currentVelocity -= _acceleration * Time.deltaTime;
                }
                else if (_currentVelocity < 0)
                {
                    _currentVelocity += _acceleration * Time.deltaTime;
                }

                /*
                if (!isGrounded)
                {
                    Quaternion desiredRotation = Quaternion.Euler(0, 0, 25);
                    transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);
                }
                */

            }
        }
        else
        {
            //Debug.Log("Out of gas");
            if (_currentVelocity > 0)
            {
                _currentVelocity -= _acceleration * Time.deltaTime;
            }
            else if (_currentVelocity < 0)
            {
                _currentVelocity += _acceleration * Time.deltaTime;
            }

            //if (_flying._isFlying) FlyDown();
        }

        //Keep on bottom of method
        //GroundedWheels = 0;


    }

    public void Forward()
    {
        Debug.Log("Gas Pressed");

        _goingBackwards = false;
        GoingForward = true;
        if(_flying == null)
        {
            Debug.Log("Flying script null");
        }
        if (_flying._isFlying)
        {
            _flying.FlightControl(false);
        }
        
        
    }

    public void ButtonRelease()
    {
        //Debug.Log("Released");
        GoingForward = false;
        _goingBackwards = false;

        if(_flying._isFlying)
        {
           
           _flying.FlightControl(true);

            
        }

        //idk if this is needed
        _currentVelocity -= _acceleration * Time.deltaTime;
    }

    public void Reverse()
    {
        GoingForward = false;
        _goingBackwards = true;
    }



    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.left * _currentVelocity, ForceMode.Force);

    }


    public IEnumerator StartingMove()
    {
        GoingForward = true;
        yield return new WaitForSeconds(2.0f);
        GoingForward = false;
    }
}
