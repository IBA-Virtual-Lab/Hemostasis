using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Pause : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuControls;
    private StarterAssetsInputs starterAssetsInputs;

    private FirstPersonController firstPersonController;
    private ThirdPersonController thirdPersonController;
    void Start()
    {
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        firstPersonController = FindObjectOfType<FirstPersonController>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        pauseMenuControls = GameObject.Find("Controls");
        resume();
    }

    public void resume(){
        pauseMenuUI.SetActive(false);
        if(pauseMenuControls){
            pauseMenuControls.SetActive(false);
        }
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(firstPersonController){
            firstPersonController.isCameraRotationEnabled = true;
        } else if(thirdPersonController){
            thirdPersonController.isCameraRotationEnabled = true;
        }
        starterAssetsInputs.cursorInputForLook = true;
    }
    public void pause(){
        pauseMenuUI.SetActive(true);
        pauseMenuControls.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(firstPersonController){
            firstPersonController.isCameraRotationEnabled = false;
        } else if(thirdPersonController){
            thirdPersonController.isCameraRotationEnabled = false;
        }
        starterAssetsInputs.cursorInputForLook = false;
    }
}