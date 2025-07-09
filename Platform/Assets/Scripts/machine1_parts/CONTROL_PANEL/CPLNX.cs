using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CPLNX : Interactable
{
    [SerializeField]
    private int number;
    private float pointerDownTimer = 0;

    private ScreenController screenController;
    void Start()
    {
        screenController = FindObjectOfType<ScreenController>();
    }

    // Runs every update when raycast of player hits object and player is pressing 'E'
    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1.0f){
            screenController.updateCPLN(number);
            screenController.updateSelectedOption(false);
            pointerDownTimer = 0.0f;
        }
    }
}