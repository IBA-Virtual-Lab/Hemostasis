using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorTrip : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public Button button;
    public GameObject panel;
    public GameObject panelToActivate;


    private void Start()
    {
        Debug.Log("Started fridge.");
    }

    public void OnButtonClick()
    {
        // Activate the game objects
        object1.SetActive(true);
        object2.SetActive(true);
        object3.SetActive(true);

        // Deactivate the panel
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Activate the second panel
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }
}
