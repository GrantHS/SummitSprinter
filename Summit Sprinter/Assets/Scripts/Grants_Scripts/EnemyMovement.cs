using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public BadGuy badGuySO;
    private float sensorLength = 50f;
    private float yOffset = 5f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.up, out hit, sensorLength);
        Debug.DrawRay(transform.position, transform.up, Color.red);

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<PlayerMovement>())
            {
                //Debug.Log("Player Detected");
                GameObject player = hit.collider.gameObject;
                player.GetComponent<PlayerCollision>().TakeDamage(badGuySO);
                switch (badGuySO.enemyType)
                {
                    case Enemies.Spikey:
                        //Debug.Log("Spikey position: " + this.transform.localPosition.y + "| Player position: " + player.transform.localPosition.y);
                        if (this.transform.localPosition.y - yOffset <= player.transform.localPosition.y)
                        {
                            //Debug.Log("adding force");
                            transform.Translate(Vector3.up * badGuySO.speed * Time.deltaTime); 
                            
                        }
                        else
                        {
                            //Debug.Log("not adding force");
                            GetComponent<BoxCollider>().isTrigger = true;
                        }
                        break;
                    case Enemies.Wheelie:
                        break;
                    case Enemies.Rocketee:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<PlayerMovement>())
        {
            switch (badGuySO.enemyType)
            {
                case Enemies.Spikey:
                    GetComponent<BoxCollider>().isTrigger = true;
                    break;
                case Enemies.Wheelie:
                    break;
                case Enemies.Rocketee:
                    break;
                default:
                    break;
            }
        }
    }


}
