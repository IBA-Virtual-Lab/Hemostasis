using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] cameraPositions; // Array to hold references to camera positions
    public Camera mainCamera;

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;
    }

    // Method to move the camera to a specific position
    public void MoveCameraToPosition(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < cameraPositions.Length)
        {
            Transform targetPosition = cameraPositions[positionIndex];
            mainCamera.transform.position = targetPosition.position;
            mainCamera.transform.rotation = targetPosition.rotation;
            Debug.Log("Camera SHOULD be assigned" + positionIndex + "value " + cameraPositions[positionIndex]);
        }
        else
        {
            Debug.LogWarning("Invalid camera position index!");
        }
    }
}
