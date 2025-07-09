using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject checkQuitPanel; // Panel to activate when exit clicked

    // Start is called before the first frame update
    public void ActivateQuitCheck()
    {
        if (checkQuitPanel != null)
        {
            checkQuitPanel.SetActive(true);
        }
        else
        {
            Debug.Log("No panel assigned to checkQuitPanel in ActivateQuickCheck!");
        }
    }
}
