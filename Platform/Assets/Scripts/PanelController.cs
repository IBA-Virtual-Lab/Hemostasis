using UnityEngine;

public class PanelController : MonoBehaviour
{
    public Canvas canvas; // Reference to your canvas goes here

    void Start()
    {
        // Ensure the canvas is initially active
        if (canvas != null)
            canvas.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        // Close the panel by deactivating the entire canvas
        if (canvas != null)
            canvas.gameObject.SetActive(false);
    }
}