using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float startSpeed = 0f; // Constant speed of the car
    public float acceleration = 10f; // Speed increase when pressing "W"
    public float deceleration = 50f; // Speed decrease when pressing "S"
    public float horizontalSpeedRatio = 0.15f;

    public GameObject tunnelPrefab; 
    public Light leftLight; 
    public Light rightLight; 
    public float tunnelSpawnProbability = 0.07f; // Probability of a tunnel spawning
    public float tunnelSpawnDistance = 500f; // where tunnel will be spawn

    public float minTunnelLength = 100f;
    public float maxTunnelLength = 300f;
    private float currentSpeed;
    private float tunnelStartPosition = -1f;
    private float tunnelEndPosition = -1f;

    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        // Move the car forward with the current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (tunnelStartPosition <= transform.position.z && transform.position.z <= tunnelEndPosition)
        {
            TurnOnCarLights();
        }
        else
        {
            TurnOffCarLights();
        }

        // Check for input to adjust speed
        HandleInput();
        MoveHorizontally();
        TrySpawnTunnel();
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
    
    
    void TrySpawnTunnel()
    {
        // Randomly decide whether to spawn a tunnel
        if (Random.value < tunnelSpawnProbability && transform.position.z > tunnelEndPosition)
        {
            // Spawn a tunnel 
            Vector3 tunnelPosition = new Vector3(0, 0f, transform.position.z + tunnelSpawnDistance);
            float tunnelLength = Random.Range(minTunnelLength, maxTunnelLength);
            tunnelStartPosition = transform.position.z + tunnelSpawnDistance - tunnelLength / 2;
            tunnelEndPosition = transform.position.z + tunnelSpawnDistance + tunnelLength / 2;
            
            GameObject tunnel = Instantiate(tunnelPrefab, tunnelPosition, Quaternion.identity);
            tunnel.transform.localScale = new Vector3(tunnel.transform.localScale.x, tunnel.transform.localScale.y, tunnelLength);
        }
    }
    
    void TurnOnCarLights()
    {
        Debug.Log("Lights On!");
        leftLight.enabled = true;
        rightLight.enabled = true;
    }
    
    void TurnOffCarLights()
    {
        Debug.Log("Lights Off!");
        leftLight.enabled = false;
        rightLight.enabled = false;
    }
}
