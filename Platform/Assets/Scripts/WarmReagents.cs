using UnityEngine;
using UnityEngine.UI;

public class ActivateGameObjects : MonoBehaviour
{
    public Button button;
    public GameObject panelToDeactivate;

    private void Start()
    {
        // Add a listener to the button click event
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {

        // Deactivate the first panel
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }

    }
}
