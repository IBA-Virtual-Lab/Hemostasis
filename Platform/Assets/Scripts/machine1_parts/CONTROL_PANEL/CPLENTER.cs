using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CPLENTER : Interactable
{
    public float pointerDownTimer;
    private MODE_TRACKER MODE;
    private ScreenController screen_controller;

    void Start()
    {
        MODE = FindObjectOfType<MODE_TRACKER>();
        screen_controller = FindObjectOfType<ScreenController>();
    }
    
    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
        // Call function in ScreenController as Enter was pressed
            screen_controller.enterWasPressed();
            pointerDownTimer = 0;
        }
    }
}
