using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCollisions : MonoBehaviour
{
    public GameObject player;

    public GameObject Merge_1;
    public GameObject Merge_2;
    public GameObject Merge_3;

    public bool Merge_1On  ;
    public bool Merge_2On  ;
    public bool Merge_3On  ;


    private void Awake()
    {
      //  Merge_1On = false;
        //Merge_2On = false;
        //Merge_3On = false;

       // Merge_1.gameObject.SetActive(false);
       // Merge_2.gameObject.SetActive(false);
       // Merge_3.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Merge_1On)
        {
            Merge_1.gameObject.SetActive(true);
        }
        else
        {
            Merge_1.gameObject.SetActive(false);
        }


        if (Merge_2On)
        {
            Merge_2.gameObject.SetActive(true);
        }
        else
        {
            Merge_2.gameObject.SetActive(false);
        }
        

        if (Merge_3On)
        {
            Merge_3.gameObject.SetActive(true);
        }
        else
        {
            Merge_3.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Merge 1"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Merged");
            Merge_1On = true;
        }

        if (other.gameObject.CompareTag("Merge 2"))
        {
            other.gameObject.SetActive(false);
            Merge_2On = true;
        }


        if (other.gameObject.CompareTag("Merge 3"))
        {
            other.gameObject.SetActive(false);
            Merge_3On = true;
        }

    }

}
