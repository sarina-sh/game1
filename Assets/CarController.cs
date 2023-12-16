using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float startSpeed = 0f; // Constant speed of the car
    public float acceleration = 10f; // Speed increase when pressing "W"
    public float deceleration = 50f; // Speed decrease when pressing "S"
    public float horizontalSpeedRatio = 0.2f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        // Move the car forward with the current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Check for input to adjust speed
        HandleInput();
        MoveHorizontally();
    }

    void HandleInput()
    {
        // Increase speed when "W" key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Decrease speed when "S" key is pressed
            currentSpeed -= deceleration * Time.deltaTime;

            // Ensure the speed doesn't go below the 0
            currentSpeed = Mathf.Max(0, currentSpeed);
        }
    }
    void MoveHorizontally()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float newPosition = transform.position.x + horizontalInput * horizontalSpeedRatio * currentSpeed * Time.deltaTime;

        // Limit the horizontal position within the specified range
        newPosition = Mathf.Clamp(newPosition, -7, 7);

        // Update the car's position
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}
