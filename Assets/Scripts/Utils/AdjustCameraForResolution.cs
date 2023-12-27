using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraForResolution : MonoBehaviour
{
    // Setting the reference resolution
    public Vector2 referenceResolution = new Vector2(1920f, 1080f);

    void Start()
    {
        // Call the function to adjust the camera size initially
        AdjustCameraSize();
    }

    // Update is called once per frame
    // void Update()
    // {
        // Call the function to adjust the camera size if the screen resolution changes
        // AdjustCameraSize();
    // }

    void AdjustCameraSize()
    {
        // Get the current screen resolution
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Set the reference resolution based on the current screen resolution
        Vector2 referenceResolution = new Vector2(1920f, 1080f);
        float targetAspect = referenceResolution.x / referenceResolution.y;
        float currentAspect = screenWidth / screenHeight;

        // Adjust the reference resolution dynamically based on the current screen aspect ratio
        if (currentAspect > targetAspect)
        {
            referenceResolution.y = referenceResolution.x / currentAspect;
        }
        else
        {
            referenceResolution.x = referenceResolution.y * currentAspect;
        }

        // Calculate the orthographic size to maintain the same world space view
        float orthographicSize = GetComponent<Camera>().orthographicSize;
        orthographicSize *= targetAspect / currentAspect;

        // Set the new orthographic size
        GetComponent<Camera>().orthographicSize = orthographicSize;
    }
}