using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // References to canvases in the original scene
    public Canvas[] originalSceneCanvases;

    // Static property to store references to canvases in the new scene
    private static List<Canvas> newSceneCanvases = new List<Canvas>();
    public static List<Canvas> NewSceneCanvases
    {
        get { return newSceneCanvases; }
    }

    // Singleton instance of the GameManager
    public static GameManager Instance;

    // Ensure only one instance of GameManager exists
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Activate canvases for the original scene
        ActivateCanvases(originalSceneCanvases);
    }

    // Method to activate canvases
    void ActivateCanvases(Canvas[] canvases)
    {
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    // Method to deactivate canvases
    void DeactivateCanvases(Canvas[] canvases)
    {
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    // Method to load a new scene
    public void LoadScene(string sceneName)
    {
        // Deactivate canvases from the original scene
        //DeactivateCanvases(originalSceneCanvases);

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Activate canvases for the new scene (you may need to wait for the scene to load first)
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Callback method invoked when the new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene matches the intended new scene
        if (scene.name == "PT-INR_Scene")
        {
            // Activate canvases for the new scene
            ActivateCanvases(newSceneCanvases.ToArray());

            // Unsubscribe from the event to prevent multiple callbacks
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
