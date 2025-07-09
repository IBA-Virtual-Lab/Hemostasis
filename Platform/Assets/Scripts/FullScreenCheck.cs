using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenCheck : MonoBehaviour
{
    [SerializeField] Canvas currentCanvas; // Main menu panel to deactivate
    [SerializeField] Canvas mainCanvas; // New panel to activate
    [SerializeField] public GameObject checkQuitPanel; // Reference to the panel object
    // Start is called before the first frame update
    public void ToMain()
    {
        Debug.Log("Proceeded past full screen");

        // Deactivate the start menu
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
            Debug.Log("full screen canvas off");

        }

        // Activate the start menu
        if (mainCanvas != null)
        {
            mainCanvas.gameObject.SetActive(true);
            Debug.Log("main canvas on");

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
