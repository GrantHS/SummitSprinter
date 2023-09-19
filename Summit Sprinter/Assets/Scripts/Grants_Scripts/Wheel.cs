using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    private float _speed = 200f;
    /*
    public bool isGrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected");
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touching Ground");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */

    private void Update()
    {
        this.transform.Rotate(0,0,_speed *  Time.deltaTime);
    }
}
