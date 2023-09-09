using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject target;
    private Vector3 _targetLocation;
    private Vector3 _trackDirection;
    private Vector3 _currentLocation;
    // Start is called before the first frame update
    void Start()
    {
        _currentLocation = this.transform.position;
        _targetLocation = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //_trackDirection = 
    }
}
