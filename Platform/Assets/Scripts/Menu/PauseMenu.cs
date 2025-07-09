using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private Pause pauseScript;
    void Start()
    {
        pauseScript = FindObjectOfType<Pause>();
    }

    public void resume(){
        pauseScript.resume();
    }

    public void exitExperiment(){
        SceneManager.LoadScene(1); // Loads lab scene
    }

    public void exit(){
        SceneManager.LoadScene(0);
    }
}
