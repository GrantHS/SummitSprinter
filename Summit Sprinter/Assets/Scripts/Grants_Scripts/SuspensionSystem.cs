using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionSystem : MonoBehaviour
{
    public GameObject wheel;
    private GameObject[] _wheelPrefabs = new GameObject[4];

    private Vector3[] _wheels = new Vector3[4];
    public Vector2 _wheelDist = new Vector2(2, 2);
    private float[] oldDist = new float[4];

    float maxSuspLength = 3f;
    float suspensionMultiplier = 120f;
    float dampSens = 500f;
    float maxDamp = 40f;

    Rigidbody rb;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            oldDist[i] = maxSuspLength;
            _wheelPrefabs[i] = Instantiate(wheel, _wheels[i], Quaternion.identity);
        }
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

        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + _wheels[i], -transform.up, out hit, maxSuspLength);
            if(hit.collider != null)
            {
                rb.AddForceAtPosition((Mathf.Clamp(maxSuspLength - hit.distance, 0, 3) * suspensionMultiplier * transform.up + transform.up * Mathf.Clamp((oldDist[i] - hit.distance) * dampSens, 0,  maxDamp)) * Time.deltaTime, _wheels[i]);

                _wheelPrefabs[i].transform.position = hit.point + transform.up * 0.5f;
                _wheelPrefabs[i].transform.rotation = transform.rotation;
            }
            else 
            {
                _wheelPrefabs[i].transform.position = transform.position + _wheels[i] - transform.up * (maxSuspLength - 0.5f);
                _wheelPrefabs[i].transform.rotation = transform.rotation;
            }

            oldDist[i] = hit.distance;
        }
    }
}
