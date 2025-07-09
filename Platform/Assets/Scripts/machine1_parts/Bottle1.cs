using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottle1 : Interactable
{
    public MODE_TRACKER MODE_TRACKER_script;
    public pipt ppt_script;
    public SPA_Preheat preheat_hole_script;

    public float pointerDownTimer;
    [SerializeField]
    private GameObject pipt;
    private GameObject SPA_Bottle;
    //private bool pipt_use;

    private const string label1 = "Reagent (SPA+)";
    private const string lable2 = "Take SPA";
    private const string lable3 = "Set Preheat";

    private float spaX;
    public bool bottle_preheat_set;

    private pipt reagentPipette;

    void Start()
    {
        bottle_preheat_set = false;
        promptMessage = label1;
        preheat_hole_script = FindObjectOfType<SPA_Preheat>();
        SPA_Bottle = GameObject.Find("spa_bottle");
        reagentPipette = FindObjectOfType<pipt>();
    }

    // Update is called once per frame
    void Update()
    {
        spaX = SPA_Bottle.transform.position.x;
        if (spaX == 0.145f){
            //spaLoc_flag = true;
            bottle_preheat_set = true;
        }
        /*
        MODE_TRACKER_script = FindObjectOfType<MODE_TRACKER>();
        ppt_script = FindObjectOfType<pipt>();
        if(ppt_script.pipt_selected == 1){
            promptMessage = lable2;
        }

        if(preheat_hole_script.preheat_selected){
            promptMessage = lable3;
        }
        else{
            promptMessage = label1;
        }
        */
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(preheat_hole_script.preheat_selected){
                preheat_hole_script = FindObjectOfType<SPA_Preheat>();
                preheat_hole_script.preheat_selected = false;
                SPA_Bottle.GetComponent<Animator>().SetTrigger("preheat_on");
                reagentPipette.reagentReady = true; // This means the reagent has been set. This is tightly knit code. Not good. (This should rather check if the reagent is set, instead of it being controlled by the reagent).
            }
            /*if(ppt_script.pipt_selected == 1){
                pipt.GetComponent<Animator>().SetTrigger("trigger_pip");
                ppt_script = FindObjectOfType<pipt>();
                ppt_script.pipt_selected = 0;
            }*/
        }
    }
}
