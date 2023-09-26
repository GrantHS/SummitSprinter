using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTVechicle : MonoBehaviour
{
    public float speed = 5.0f; // Forward movement speed.
    public float maxSpeed = 10.0f; // Maximum speed.
    public float acceleration = 10.0f; // Acceleration rate.
    public float brakeForce = 20.0f; // Braking force.
    public float suspensionForce = 5.0f; // Suspension force.
    public float suspensionRange = 0.2f; // Suspension range.

    private Rigidbody rb;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, suspensionRange);

        if (isGrounded)
        {
            // Apply suspension force to keep the vehicle grounded.
            rb.AddForce(Vector3.up * suspensionForce);
        }

        // Input handling for acceleration and braking.
        float moveInput = Input.GetAxis("Vertical");
        float brakeInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;

        // Calculate the current speed in the forward direction.
        float currentSpeed = Vector3.Dot(rb.velocity, transform.forward);

        // Apply acceleration or braking.
        float targetSpeed = moveInput * maxSpeed;
        float accelerationForce = (targetSpeed - currentSpeed) * acceleration;

        // Apply braking force.
        if (brakeInput > 0.0f)
        {
            rb.AddForce(-transform.forward * brakeForce * brakeInput);
        }

        // Apply acceleration force.
        rb.AddForce(transform.forward * accelerationForce);

        // Limit the maximum speed.
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
