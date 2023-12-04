using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public BadGuy badGuySO;
    private float sensorLength = 50f;
    private float yOffset = 5f;
    private float despawnTime = 1f;
    private float respawnTime = 5f;
    private Rigidbody rb;
    private GameObject player;
    private bool attacked = false; //did the enemy attack already?

    //floatie parameters
    private bool isFloating = false;
    private float floatOffset = 5f;

    [Tooltip("only needed for floatie enemies")]
    public GameObject maxY;
    [Tooltip("only needed for floatie enemies")]
    public GameObject minY;

    private float floatTime = 3f;

    Vector3 yPos = Vector3.zero;





    private void Awake()
    {
        yPos.y = this.transform.position.y;

        rb = GetComponent<Rigidbody>();
        if(badGuySO.enemyType != Enemies.Floatie) rb.isKinematic = true;
    }

    private void Update()
    {
        switch (badGuySO.enemyType)
        {
            case Enemies.Spikey:
                /*
                //Debug.Log("Spikey position: " + this.transform.localPosition.y + "| Player position: " + player.transform.localPosition.y);
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.up, out hit, sensorLength);
                Debug.DrawRay(transform.position, transform.up, Color.red);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<PlayerMovement>())
                    {
                        //Debug.Log("Player Detected");
                        player = hit.collider.gameObject;
                        //player.GetComponent<PlayerCollision>().TakeDamage(badGuySO);

                    }
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
                }
                */
                break;
            case Enemies.Wheelie:
                break;
            case Enemies.Looney:
                break;
            case Enemies.Floatie:
                if (!isFloating) StartCoroutine(Float());
                break;
            default:
                break;
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<Wheel>())
        {
            player = collision.collider.gameObject.GetComponent<Wheel>().Player;
          
            switch (badGuySO.enemyType)
            {
                case Enemies.Spikey:

                    SpikeyAttack(player);
                    //GetComponent<BoxCollider>().isTrigger = true;
                    if (!attacked)
                    {
                        //player.GetComponent<PlayerMovement>().gasMeter.GasDrain += badGuySO.damage;
                        
                        //attacked = true;
                    }                   
                    break;
                case Enemies.Wheelie:
                    StartCoroutine(Ragdoll(player));
                    break;
                case Enemies.Looney:
                    StartCoroutine(Ragdoll(player));
                    break;
                case Enemies.Floatie:
                    SpikeyAttack(player);
                    break;
                default:
                    break;
            }
        }

        if (collision.collider.gameObject.GetComponent<PlayerMovement>())
        {
            player = collision.collider.gameObject.GetComponent<PlayerMovement>().gameObject;

            switch (badGuySO.enemyType)
            {
                case Enemies.Spikey:

                    SpikeyAttack(player);
                    //GetComponent<BoxCollider>().isTrigger = true;
                    if (!attacked)
                    {
                        //player.GetComponent<PlayerMovement>().gasMeter.GasDrain += badGuySO.damage;

                        //attacked = true;
                    }
                    break;
                case Enemies.Wheelie:
                    StartCoroutine(Ragdoll(player));
                    break;
                case Enemies.Looney:
                    StartCoroutine(Ragdoll(player));
                    break;
                case Enemies.Floatie:
                    SpikeyAttack(player);
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator Float()
    {
        Debug.Log(minY);
        Debug.Log(maxY);
        isFloating = true;
        bool goingUp = transform.position.y <= minY.transform.position.y;

        if(transform.position.y >= maxY.transform.position.y)
        {
            goingUp = false;
            
        }    
        if(transform.position.y <= minY.transform.position.y) 
        {
            goingUp = true;
        }

        if(goingUp)
        {
            Debug.Log("Going Up");
            rb.velocity = Vector3.up * badGuySO.speed;

            /*
            yPos += Vector3.up * badGuySO.speed * Time.deltaTime;
            transform.position += yPos;
            */
        }
        else
        {
            Debug.Log("Going Down");
            rb.velocity = Vector3.down * badGuySO.speed;

            /*
            yPos += Vector3.down * badGuySO.speed * Time.deltaTime;
            transform.position += yPos;
            */
        }

        yield return new WaitForSeconds(floatTime);
        isFloating = false;
        
    }

    private IEnumerator EnemyRespawn()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(respawnTime);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void SpikeyAttack(GameObject player)
    {
        player.GetComponent<PlayerMovement>().gasMeter.GasDrain += badGuySO.damage;
        Debug.Log("Attacked Player");
        StartCoroutine(EnemyRespawn());
        //attacked = true;
    }

    private IEnumerator Ragdoll(GameObject player)
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.left * badGuySO.speed, ForceMode.Impulse);
        player.GetComponent<PlayerMovement>().gasMeter.currentValue -= badGuySO.damage;
        yield return new WaitForSeconds(despawnTime);
        gameObject.SetActive(false);

    }



    

}
