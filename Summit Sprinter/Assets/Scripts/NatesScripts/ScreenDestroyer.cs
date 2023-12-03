using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDestroyer : MonoBehaviour
{

    private void Update()
    {
        // You can customize the speed of the bullet as needed
        float bulletSpeed = 50f;
        // Vector3 direction = Vector3.left * 1; // Use the forward direction of the firePoint

        Vector3 direction = Vector3.left;
        this.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spikey"))
        {
            Destroy(other.gameObject);
        }
    }

}
