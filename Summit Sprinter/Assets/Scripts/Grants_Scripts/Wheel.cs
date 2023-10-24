using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public bool isGrounded = false;
    private float speed = 30f;


    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void Update()
    {
        gameObject.transform.Rotate(0,0,-speed * Time.deltaTime);
    }
}
