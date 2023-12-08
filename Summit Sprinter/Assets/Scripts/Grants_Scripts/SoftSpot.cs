using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.OnGameLose();
            
        }
    }
}
