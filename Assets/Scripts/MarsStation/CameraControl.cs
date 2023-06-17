using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public float panSpeed = 10f;
    public float panBorderThickness = 10f;
    public float fastPanSpeedMultiplier = 2f;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;

    private Camera mainCamera;
    private float currentPanSpeed;

    private void Start()
    {
        mainCamera = Camera.main;
        currentPanSpeed = panSpeed;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        // Check if the mouse position is near the screen edges
        bool mouseNearTop = Input.mousePosition.y >= Screen.height - panBorderThickness;
        bool mouseNearBottom = Input.mousePosition.y <= panBorderThickness;
        bool mouseNearRight = Input.mousePosition.x >= Screen.width - panBorderThickness;
        bool mouseNearLeft = Input.mousePosition.x <= panBorderThickness;

        // Calculate the camera's movement direction based on the mouse position
        Vector3 panDirection = Vector3.zero;
        if (mouseNearTop && transform.position.z < boundaryMax.y)
            panDirection += Vector3.forward;
        if (mouseNearBottom && transform.position.z > boundaryMin.y)
            panDirection += Vector3.back;
        if (mouseNearRight && transform.position.x < boundaryMax.x)
            panDirection += Vector3.right;
        if (mouseNearLeft && transform.position.x > boundaryMin.x)
            panDirection += Vector3.left;

        // Move the camera based on the panDirection and currentPanSpeed
        transform.Translate(panDirection.normalized * currentPanSpeed * Time.deltaTime, Space.World);

        // Move the camera using WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 wasdDirection = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(wasdDirection.normalized * currentPanSpeed * Time.deltaTime, Space.World);

        // Check if Left Shift key is held down to increase pan speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentPanSpeed = panSpeed * fastPanSpeedMultiplier;
        }
        else
        {
            currentPanSpeed = panSpeed;
        }

        // Clamp the camera's position within the specified boundary
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boundaryMin.x, boundaryMax.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, boundaryMin.y, boundaryMax.y);
        transform.position = clampedPosition;
    }
}