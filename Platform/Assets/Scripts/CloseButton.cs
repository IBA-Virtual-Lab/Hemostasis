using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public GameObject CanvasToActivate;
    public GameObject modalPanel;
    public Button button;

    public void Start()
    {
        // Ensure the modalPanel is not null
        if (modalPanel == null)
        {
            Debug.Log("Modal Panel reference not set in CloseButtonScript.");
        }

        if (CanvasToActivate == null)
        {
            Debug.Log("Modal Panel reference not set in CloseButtonScript.");
        }

    }

    public void OnButtonClick()
    {
        // Deactivate the panel
        if (modalPanel != null)
        {
            CloseModalPanel();
        }
    }

    public void CloseModalPanel()
    {
        // Check if the modalPanel is not null before closing
        if (modalPanel != null)
        {
            modalPanel.SetActive(false);
            CanvasToActivate.SetActive(false);
        }
        else
        {
            Debug.Log("Modal Panel or Canvas reference is null in CloseButtonScript.");
        }
    }
}
