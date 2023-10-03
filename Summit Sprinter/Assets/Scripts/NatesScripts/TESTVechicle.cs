using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTVechicle : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Transform tireTransform;
    private bool rayDidHit;

    private Vector3 springDir;
    public float suspensionRestDist;
    public float suspensionTravel;
    public float springStrength;
    public float springDamper;

    private RaycastHit tireRay;


    private void Awake()
    {
        suspensionRestDist = 1f;
        suspensionTravel = 25f;
        springStrength = 100f;
        springDamper = 30f;
    }
    private void Update()
    {

    RaycastHit hit;
    Vector3 rayOrigin = tireTransform.position;
    Vector3 rayDirection = -tireTransform.up; // Assuming the tires are facing downward
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, suspensionRestDist))
        {
            // Handle the raycast hit here
            Debug.DrawLine(rayOrigin, hit.point, Color.green);
            rayDidHit = true;// Draw a green line for visualization
        }
        else
        {
            // Handle the case when the raycast doesn't hit anything
            Debug.DrawRay(rayOrigin, rayDirection * suspensionRestDist, Color.red);
            rayDidHit = false;// Draw a red line for visualization
        }
        if (rayDidHit)
        {
            //world space velocity of this tire
            Vector3 tireWorldVel = carRigidbody.GetPointVelocity(tireTransform.position);

            //calculate offset from the raycast
            float offset = suspensionRestDist - tireRay.distance;

           
            //calculate velocity along the spring direction
            //note that springDir is a unit vector, so this returns the magnitude of tireWorldVel
            //as projected onto springDir
            float vel = Vector3.Dot(springDir, tireWorldVel);

            //calculate the magnitude of the damped spring force!
            float force = (offset * springStrength) - (vel * springDamper);

            //apply the force at the location of this tire, in the direction
            // of the suspension
            carRigidbody.AddForceAtPosition(springDir * force, tireTransform.position);



        }

    }


  
    /*
    private void Suspension()
    {
        if (rayDidHit)
        {
            //world space velocity of this tire
            Vector3 tireWorldVel = carRigidbody.GetPointVelocity(tireTransform.position);

            //calculate offset from the raycast
            float offset = suspensionRestDist - tireRay.distance;

            CalculateRaycastOffset(tireTransform, suspensionRestDist);

            //calculate velocity along the spring direction
            //note that springDir is a unit vector, so this returns the magnitude of tireWorldVel
            //as projected onto springDir
            float vel = Vector3.Dot(springDir, tireWorldVel);

            //calculate the magnitude of the damped spring force!
            float force = (offset * springStrength) - (vel * springDamper);

            //apply the force at the location of this tire, in the direction
            // of the suspension
            carRigidbody.AddForceAtPosition(springDir * force, tireTransform.position);



        }
    }


    float CalculateRaycastOffset(Transform originTransform, float suspensionRestDist)
    {
        RaycastHit hit;
        Vector3 rayOrigin = originTransform.position;
        Vector3 rayDirection = -originTransform.up; // Assuming the object is oriented downward
        float rayLength = suspensionRestDist + suspensionTravel; // Adjust as needed

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            // Calculate the offset as the difference between the suspension rest distance
            // and the distance to the hit point
            float offset = suspensionRestDist - hit.distance;
            return offset;
        }

        // If the raycast didn't hit anything, return a default offset (e.g., suspensionRestDist).
        return suspensionRestDist;
    }
    */
}
