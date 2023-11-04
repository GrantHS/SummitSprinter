using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private GameObject target;

    [Range(2f, 10f)] //a max of 10 prevents level camera from viewing bottom of level
    [SerializeField]
    [Tooltip("The size of the camera")]
    private float _trackingDistance = 5f;

    /*
    [SerializeField]
    
    private float _trackingHeight;
    */

    private Vector3 _targetLocation;
    private Vector3 _trackDirection;
    private Vector3 _currentLocation;

    private Vector3 _cameraOffset;

    // Start is called before the first frame update
    private void Awake()
    {
        target = FindObjectOfType<PlayerMovement>().gameObject;
        _cameraOffset = new Vector3(-6.50f, 0, 10f);
    }
    void Start()
    {
        
        _targetLocation = target.transform.position;
        _currentLocation = target.transform.position + _cameraOffset;
        //_currentLocation.z = _trackingDistance;
        this.transform.position = _currentLocation;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Camera>().orthographicSize = _trackingDistance;
        _currentLocation = this.transform.position;
        _targetLocation = target.transform.position + _cameraOffset;
        _trackDirection = _targetLocation - _currentLocation;
        _trackDirection.z = 0;
        _currentLocation += _trackDirection;
        this.transform.position = _currentLocation;
    }
}
