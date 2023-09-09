using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject target;

    //[SerializeField]
    //[Tooltip("How far on the Z-axis this object will follow.")]
    private float _trackingDistance;

    private Vector3 _targetLocation;
    private Vector3 _trackDirection;
    private Vector3 _currentLocation;
    // Start is called before the first frame update
    void Start()
    {

        _trackingDistance = this.transform.position.z;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentLocation = this.transform.position;
        _targetLocation = target.transform.position;
        _trackDirection = _targetLocation - _currentLocation;
        _trackDirection.z = 0;
        _currentLocation += _trackDirection;
        this.transform.position = _currentLocation;
    }
}
