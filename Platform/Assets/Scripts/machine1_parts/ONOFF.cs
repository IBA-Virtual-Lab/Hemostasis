using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ONOFF : Interactable
{
    public bool system_on; // Used by screen_controller and others to determine system is on or off
    
    private float pointerDownTimer;
    private const string label1 = "SWITCH ON";
    private const string label2 = "SWITCH OFF";
    private ScreenController screenController;
    // Start is called before the first frame update
    void Start()
    {   
        screenController = GameObject.Find("STart4").GetComponent<ScreenController>();
        // Mounted on Machine1 object
        system_on = false;
        promptMessage = label1;
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            system_on = !system_on;
            if(system_on){
                promptMessage = label2;
                screenController.physicalScreen.GetComponent<MeshRenderer>().material = screenController.physicalScreenMaterialOn;
            }else if(!system_on){ // If screen is off
                screenController.screenOff();
                promptMessage = label1;
            }
            pointerDownTimer = 0;
        }
    }
}
