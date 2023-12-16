using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed of movement
    public float laneWidth = 3f; // Adjust the width of each lane

    void Start()
    {
        
    }

    void Update()
    {
        // Get input from keys
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement
        Vector3 movement = transform.forward * verticalInput;

        // Move the car forward or backward
        transform.Translate(movement * speed * Time.deltaTime);

        // Change lanes left or right
        if (horizontalInput != 0f)
        {
            float targetLane = Mathf.Round(transform.position.x / laneWidth) * laneWidth;
            float horizontalMovement = Mathf.MoveTowards(transform.position.x, targetLane, speed * Time.deltaTime);
            transform.position = new Vector3(horizontalMovement, transform.position.y, transform.position.z);
        }
    }
}
