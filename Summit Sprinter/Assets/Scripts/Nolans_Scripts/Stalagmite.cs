using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    private GameObject Stalagmites;
    public Animator StalAnimationController;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StalAnimationController.SetBool("Open", true);
        }
    }
}
