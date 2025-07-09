using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPLA_PREHEAT : Interactable
{
    public bool preheatDone;
    public bool preheatStarted;

    private float pointerDownTimer;
    private float preheatTimer;
    private float preheatTime;
    // Start is called before the first frame update
    void Start()
    {
        preheatDone = false;
        preheatStarted = false;
        pointerDownTimer = 0;

        preheatTimer = 0;
        preheatTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // use this with deltatime to count down. Show it somewhere in the UI.
        if(preheatStarted){
            preheatTimer += Time.deltaTime;
            if(preheatTimer > preheatTime){
                preheatDone = true;
                preheatStarted = false;
                preheatTimer = 0;
            }
        }
    }

    protected override void Interact()
    {   
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            Debug.Log("(NOT IMPLEMENTED) - Started Preheat timer of 180 seconds");
            preheatStarted = true;
            pointerDownTimer = 0;
        }
    }
}
