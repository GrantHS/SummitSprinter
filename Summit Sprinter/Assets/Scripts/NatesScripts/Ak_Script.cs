using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_Script : MonoBehaviour
{
    public GameObject bulletPreFab;
    public Transform GunBarrel;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private float RateOfFire;


    private bool canFire = true;
    private bool shooting = true;

    private void Update()
    {
        if (shooting) 
        {
            if (canFire) // later make it not possible for player to shoot under certain circumstances
            {
                StartCoroutine(Fire());
            }
            
        }
    }

    private IEnumerator Fire()
    {
        shooting = false;
        canFire = false;

        GameObject bullet = Instantiate(bulletPreFab, GunBarrel.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = -GunBarrel.forward * BulletSpeed;
        }
        bullet.GetComponent<Renderer>().material.color = Color.red; 
        yield return new WaitForSeconds(RateOfFire);
      //  bullet.GetComponent<Renderer>().material.color = Color.white;

       // Destroy(gameObject);

        shooting = true;
        canFire = true;
    }
}
