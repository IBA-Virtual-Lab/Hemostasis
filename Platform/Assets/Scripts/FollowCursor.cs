using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Get the current position of the cursor in screen coordinates
        Vector3 cursorPosition = Input.mousePosition;

        // Convert the screen coordinates to world coordinates
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);

        // Set the position of the panel to the converted world coordinates
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}