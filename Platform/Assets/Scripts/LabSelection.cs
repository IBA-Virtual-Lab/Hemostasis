using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabSelection : MonoBehaviour
{
    [SerializeField] Canvas currentCanvas; // Main menu panel to deactivate
    [SerializeField] Canvas ptinrCanvas; // New panel to activate
    [SerializeField] Canvas apttlabCanvas; // 
    [SerializeField] Canvas fibrinogenlabCanvas; // 
    [SerializeField] public GameObject checkQuitPanel; // Reference to the panel object

    public void ptinr_info()
    {
        Debug.Log("Start PTINR pushed");

        // Deactivate the start menu
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
            Debug.Log("start canvas off");

        }

        // Activate the start menu
        if (ptinrCanvas != null)
        {
            ptinrCanvas.gameObject.SetActive(true);
            Debug.Log("PTINR Info on");

        }
    }

    public void aptt_info()
    {
        Debug.Log("Other lab pushed");

        // Deactivate the panel
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
            Debug.Log("Canvas off");

        }

        if (apttlabCanvas != null)
        {
            apttlabCanvas.gameObject.SetActive(true);
            Debug.Log("Other canvas on");
        }
    }

    public void fibrinogen_info()
    {
        Debug.Log("Other lab pushed");

        // Deactivate the panel
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
            Debug.Log("Canvas off");

        }

        if (fibrinogenlabCanvas != null)
        {
            fibrinogenlabCanvas.gameObject.SetActive(true);
            Debug.Log("Other canvas on");
        }
    }

    public void ActivateQuitCheck()
    {
        if (checkQuitPanel != null)
        {
            checkQuitPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("No panel assigned to checkQuitPanel!");
        }
    }

    public void DeactivateQuitCheck()
    {
        if (checkQuitPanel != null)
        {
            checkQuitPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("No panel assigned to checkQuitPanel");
        }
    }

    public void Exit()
    {
        //#if UNITY_EDITOR
        //        // In the editor, stop play mode
        //        UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //        // In a built application, quit the game
        //        Application.Quit();
        //#endif

        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif

        #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            Application.OpenURL("https://play.unity.com/en/games/8d3668e0-5571-4aa9-be0b-1cf747a488f0/hemostasis-training-simulation");
#endif
    }
}
