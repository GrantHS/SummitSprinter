using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftSpot : MonoBehaviour
{
    private PlayerCollision playerCollision;

    private void Awake()
    {
        playerCollision = GetComponentInParent<PlayerCollision>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.Respawn(GetComponentInParent<GameObject>(), playerCollision.respawnTime, playerCollision.spawnPos, playerCollision.spawnRot);

            if(GetComponentInParent<PlayerMovement>() != null)
            {
                Debug.Log("null player");

            }
            
        }
    }
}
