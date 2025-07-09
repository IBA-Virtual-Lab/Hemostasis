using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SCREEN : Interactable
{
    private bool screenOn;
    private float pointerDownTimer;
    private ONOFF machineOnOff;
    private ScreenController screenController;

    void Start()
    {
        screenOn = false;
        machineOnOff = FindObjectOfType<ONOFF>();
        screenController = FindObjectOfType<ScreenController>();
        screenController.screenOff(); // Ensure screen is off
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        // Toggles screen on/off
        if(pointerDownTimer > 1){
            if (!screenOn && machineOnOff.system_on){
                screenController.screenOn();
            }else{
                screenController.screenOff();
                screenOn = false;
            }
            pointerDownTimer = 0;
        }
    }
}
