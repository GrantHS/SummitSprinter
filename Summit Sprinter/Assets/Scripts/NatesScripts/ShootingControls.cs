 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingControls : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Set the allowed range for the gun's z-axis translation
    private float minZ = -90f;
    private float maxZ = 90f;

    void Start()
    {
        // Assuming the Main Camera is used for input. Adjust accordingly if needed.
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Set the distance from the camera to the gun (adjust as needed)
            float distance = 10f;

            // Combine mouse position with distance from the camera
            Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, distance);

            // Convert the target position to world space
            targetPosition = mainCamera.ScreenToWorldPoint(targetPosition);

            // Ensure the gun's "front" is aligned with the direction only on the z-axis
            Vector3 localTarget = transform.TransformPoint(targetPosition);
            localTarget = transform.InverseTransformPoint(localTarget);
            float angle = Mathf.Atan2(localTarget.y, localTarget.x) * Mathf.Rad2Deg;

            // Map the y-axis position of the mouse to the z-axis rotation of the gun
            float mouseY = Mathf.Clamp(mousePosition.y / Screen.height, 0f, 1f); // Normalize mouseY between 0 and 1
            float desiredZRotation = Mathf.Lerp(minZ, maxZ, mouseY);

            // Set the z-axis rotation within the desired range and flip it
            transform.localRotation = Quaternion.Euler(0f, 0f, -angle); // Keep the x and y rotations
            transform.Rotate(Vector3.forward, -desiredZRotation, Space.Self);

            StartCoroutine(ShootBullet());
        }
    }


    IEnumerator ShootBullet()
    {
        // Instantiate a bullet prefab at the firePoint position without changing its rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

     
        // Adjust this delay based on how quickly you want to shoot bullets
        yield return new WaitForSeconds(0.1f);

        // Optionally, you can destroy the bullet after a certain time
        Destroy(bullet, 2f);
    }
}
