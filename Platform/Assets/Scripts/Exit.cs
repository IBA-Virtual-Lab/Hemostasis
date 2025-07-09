using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Canvas specificCanvasToDeactivate;
    [SerializeField] private Canvas specificCanvasToActivate;

    private Canvas parentCanvas;

    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnButtonClick()
    {
        if (specificCanvasToDeactivate != null)
        {
            this.gameObject.SetActive(false); // Deactivate the GameObject this script is attached to
            specificCanvasToDeactivate.gameObject.SetActive(false);
        }
        else if (parentCanvas != null)
        {
            this.gameObject.SetActive(false); // Deactivate the GameObject this script is attached to
            parentCanvas.gameObject.SetActive(false);
        }

        if (specificCanvasToActivate != null)
        {
            specificCanvasToActivate.gameObject.SetActive(true);
        }
        else if (parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(true);
        }
    }

    public void DeactivatePanel()
    {
        this.gameObject.SetActive(false); // Deactivate the GameObject this script is attached to
    }
}
