using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDestroyer : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(destroy());
    }
   
    
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
