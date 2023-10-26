using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class SuspensionSystem : MonoBehaviour
{/*
    private float minYPos;
    private float maxYPos;
    public float maxSuspLength;

    private void Awake()
    {
        minYPos = transform.position.y
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(minWheelPos, maxWheelPos-transform.up, out hit, maxSuspLevel);
        Debug.DrawRay(minWheelPos, maxWheelPos - transform.up, Color.red);
    }
    */


















   
    public GameObject wheel;
    public GameObject[] _wheelPrefabs = new GameObject[4];
    public Transform[] _wheelPos = new Transform[4];
    private Vector3 orginDiff = new Vector3(2.6f, 0, 0);

    public Vector3[] _wheels = new Vector3[4];

    public Vector2 _wheelDist = new Vector2(2, 2);
    private float[] oldDist = new float[4];

    public float maxSuspLength = 2.8f;
    public float suspensionMultiplier = 300f;
    public float dampSens = 500f;
    public float maxDamp = 40f;

    public bool isGrounded;
    public int groundedWheels
    {
        get;
        private set;
    }

    Rigidbody rb;

    private void OnEnable()
    {
        for (int i = 0; i < 4; i++)
        {
            oldDist[i] = maxSuspLength;
            _wheelPrefabs[i] = Instantiate(wheel, _wheels[i], Quaternion.identity);
            //_wheelPrefabs[i].AddComponent<Wheel>();
        }

    }

    private void OnDisable()
    {
        //ArrayUtility.Clear(ref _wheelPrefabs);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _wheels[0] = transform.right * _wheelDist.x + transform.forward * _wheelDist.y; //front right
        _wheels[1] = transform.right * -_wheelDist.x + transform.forward * _wheelDist.y; //front left
        _wheels[2] = transform.right * _wheelDist.x + transform.forward * -_wheelDist.y; //back right
        _wheels[3] = transform.right * -_wheelDist.x + transform.forward * -_wheelDist.y; //back left


        groundedWheels = 0;
        for (int i = 0; i < 4; i++)
        {
            
            //Debug.Log(transform.position - orginDiff + _wheels[i]);
            RaycastHit hit;
            //Physics.Raycast(transform.position - orginDiff + _wheels[i], -transform.up, out hit, maxSuspLength);
            Physics.Raycast(_wheelPos[i].position + _wheels[i], -transform.up, out hit, maxSuspLength);
            //Debug.DrawRay(transform.position - orginDiff + _wheels[i], -transform.up, Color.red);
            Debug.DrawRay(_wheelPos[i].position + _wheels[i], -transform.up, Color.red);

            if (hit.collider != null)
            {
                
                
                rb.AddForceAtPosition((Mathf.Clamp(maxSuspLength - hit.distance, 0f, 1f) * suspensionMultiplier * transform.up + transform.up * Mathf.Clamp((oldDist[i] - hit.distance) * dampSens, 0, maxDamp)) * Time.deltaTime, transform.position + _wheels[i]);

                _wheelPrefabs[i].transform.position = hit.point + transform.up * 0.5f;
                _wheelPrefabs[i].transform.rotation = transform.rotation;
                groundedWheels++;
                
            }
            else
            {
                //Debug.Log("null collider");
                _wheelPrefabs[i].transform.position = transform.position - orginDiff + _wheels[i] - transform.up * (maxSuspLength - 0.5f);
                _wheelPrefabs[i].transform.rotation = transform.rotation;
                //isGrounded = false;
            }

            //Debug.Log("Grounded: " + isGrounded);

            oldDist[i] = hit.distance;
        }

        //Debug.Log("Grounded Wheels: " + groundedWheels);
    }

    GameObject FindChildrenWithTag(GameObject parent, string tag)
    {
        GameObject child = null;


        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
    

}
