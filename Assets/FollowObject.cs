using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // Reference to the object to follow
    public float smoothSpeed = 0.125f; // Adjust the smoothness of camera movement
    public float distance = 5f; // Adjust the distance between the camera and the object

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position - target.forward * distance;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target); // Make the camera look at the target
        }
    }
}