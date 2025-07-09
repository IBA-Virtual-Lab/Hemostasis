using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_PTINR : MonoBehaviour
{
    [SerializeField] private Canvas canvasToDeactivate;
    [SerializeField] private Canvas gamePlayCanvas;
    [SerializeField] private Canvas toolTipCanvas;
    [SerializeField] private Canvas returnCanvas;
    [SerializeField] private GameObject Lighting;
    public string sceneName; // Name of the scene to load
    [SerializeField] private GameObject[] gameObjectsToManage;

    /// <summary>
    /// Activates all GameObjects in the list.
    /// </summary>
    public void ActivateGameObjects()
    {
        foreach (GameObject obj in gameObjectsToManage)
        {
            if (obj != null) obj.SetActive(true);
        }
    }

    /// <summary>
    /// Deactivates all GameObjects in the list.
    /// </summary>
    public void DeactivateGameObjects()
    {
        foreach (GameObject obj in gameObjectsToManage)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the active state of all GameObjects in the list.
    /// </summary>
    public void ToggleGameObjects()
    {
        foreach (GameObject obj in gameObjectsToManage)
        {
            if (obj != null) obj.SetActive(!obj.activeSelf);
        }
    }
    //public GameManager gameManager;

    public void startGame()
    {
        Debug.Log("Game started!");
        //GameManager.Instance.LoadScene("PT-INR_Scene");
        if (canvasToDeactivate != null)
        {
            canvasToDeactivate.gameObject.SetActive(false);
        }

        if (gamePlayCanvas != null)
        {
            gamePlayCanvas.gameObject.SetActive(true);
        }

        //if (toolTipCanvas != null)
        //{
        //    toolTipCanvas.gameObject.SetActive(true);
        //}
    }
    public void returnToMenu()
    {
        Debug.Log("Back to square one!");
        if (canvasToDeactivate != null)
        {
            canvasToDeactivate.gameObject.SetActive(false);
        }

        if (returnCanvas != null)
        {
            returnCanvas.gameObject.SetActive(true);
        }
    }
}
