using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool readyForTakeoff = true;
    private float airTorque = 20f;
    private float _velocity = 0f;
    private float _topSpeed = 5f;
    private float _acceleration = 1f;
    private float shutOffTime = 2f;
    private Vector3 flyDirection = new Vector3(1, 200, 0);
    private float maxAngle = -25f;
    private float minAngle = 40f;
    private float _angle;
    private Quaternion _rotation;

    public float AirTorque { get => airTorque; private set => airTorque = value; }

    private void OnEnable()
    {
        //_rotation = transform.rotation;
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
        rb.useGravity = true;
    }

    private void Update()
    {
        if (playerMovement.isGrounded)
        {
            _isFlying = false;
            readyForTakeoff = true;
        }
        else if(playerMovement.GroundedWheels == 0) _isFlying = true;
        

        if (hasWings && _isFlying)
        {
            rb.useGravity = false;
            //rb.freezeRotation = true;
            foreach (var wheel in wheels)
            {
                if (!wheel.isGrounded && readyForTakeoff)
                {
                    wheel.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    _isFlying = false;
                    ChangeGravity(true);
                    break;
                }
            }
            

            //transform.rotation = _rotation;
            //if (_isFlying) Accelerate();

        }
        else if (hasWings && !_isFlying )
        {
            FlightControl(true);
        }
    }

    public void FlightControl(bool landing)
    {
        if (landing) StartCoroutine(CutEngine());
        else Fly();
    }
    private IEnumerator CutEngine()
    {
        Debug.Log("Stopping Engine");
        hasWings = false;
        readyForTakeoff = false;
        //_isFlying = false;
        GravityControl(true);
        //rb.isKinematic = true;
        yield return new WaitForSeconds(shutOffTime);
        hasWings = true;
    }

    private void Fly()
    {
        //Debug.Log("Stopping Engine");
        //rb.isKinematic = true;
        hasWings = true;
        _isFlying = true;
        Accelerate();
        GravityControl(false);



        //Debug.Log(rb.velocity);

        /*
         * 

       // Convert velocity to an angle. (If velocity.x is 0, use 1 instead)
            float newAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);

            // Convert to degrees, and clamp between your desired range.
            newAngle = Mathf.Clamp(newAngle * Mathf.Rad2Deg, 1, -1);

            // Blend from your old angle toward the new angle, smoothly.
            _angle = Mathf.Lerp(_angle, newAngle, Time.deltaTime);

        // Set your rotation to this angle.
        Quaternion desiredRotation = Quaternion.Euler(0, 0, _angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);
        //transform.localEulerAngles = new Vector3(0, 0, _angle);
            

        
        */

        //else 
        hasWings = true;
    }

    private void Accelerate()
    {
        Debug.Log(_velocity);
        rb.AddForce(flyDirection * _velocity * Time.deltaTime, ForceMode.Force);
        //rb.velocity += Vector3.left * _velocity * Time.deltaTime;

        if (_velocity < _topSpeed)
        {
            _velocity += _acceleration;
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
            //rb.useGravity = true;
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