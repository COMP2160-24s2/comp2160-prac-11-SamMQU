using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10f; // The speed of zooming
    [SerializeField] private float minZoom = 5f;    // Minimum zoom limit
    [SerializeField] private float maxZoom = 60f;   // Maximum zoom limit

    private Camera cam;
    private InputAction zoomAction;

    void Awake()
    {
        cam = Camera.main; // Get the main camera

        // Reference to your Input Actions asset (replace 'Actions' with the name of your Input Action class)
        var actions = new Actions(); 
        zoomAction = actions.camera.zoom; // The zoom action from your input map
    }

    void OnEnable()
    {
        zoomAction.Enable(); // Enable the zoom action
    }

    void OnDisable()
    {
        zoomAction.Disable(); // Disable the zoom action
    }

    void Update()
    {
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        // Read the scroll input value for analog control type
        float scrollInput = zoomAction.ReadValue<float>();
        Debug.Log("Scroll input: " + scrollInput);  // Print the input value to check

        if (scrollInput != 0f)
        {
            // Zoom for Orthographic Camera
            if (cam.orthographic)
            {
                cam.orthographicSize -= scrollInput * zoomSpeed * Time.deltaTime;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                // Zoom for Perspective Camera
                cam.fieldOfView -= scrollInput * zoomSpeed * Time.deltaTime;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
            }
        }
    }
}