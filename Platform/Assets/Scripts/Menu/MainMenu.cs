using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate; // Reference to the panel object

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ActivatePanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
        else
        {
            Debug.LogError("No panel assigned to panelToActivate!");
        }
    }

    public void DeactivatePanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(false);
        }
        else
        {
            Debug.LogError("No panel assigned to panelToActivate!");
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        // In the editor, stop play mode
         UnityEditor.EditorApplication.isPlaying = false;
#else
        // In a built application, quit the game
        Application.Quit();
#endif
    }
}