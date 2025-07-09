using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Fibr : MonoBehaviour
{
    [SerializeField] private Canvas canvasToDeactivate;
    [SerializeField] private Canvas gamePlayCanvas;
    [SerializeField] private Canvas toolTipCanvas;
    [SerializeField] private Canvas returnCanvas;
    public string sceneName; // Name of the scene to load
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
