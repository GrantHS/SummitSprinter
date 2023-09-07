using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _playerSpeed = 25f;
    private InputAction _move;
    //private InputAction _reverse;
    private Vector2 _moveDirection = Vector2.zero;

    public PlayerInputActions playerControls;
    public Rigidbody rb;

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
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(_moveDirection * _playerSpeed, ForceMode.Force);
        //rb.AddTorque(_moveDirection * _playerSpeed, ForceMode.Force);
    }



    // Update is called once per frame
    void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
        Debug.Log(_moveDirection);
    }
}
