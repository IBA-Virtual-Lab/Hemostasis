using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MODE_TRACKER : Interactable
{

    private float spaX_pos;
    private Vector3 spa_pos;
    private finalcube finalcube_script;
    private ONOFF on_off_script;
    private SPA_Preheat preheat_hole_script;
    private pipt ppt1_script;
    private SamplePipette plasmaPipette;
    private pipt3 ppt3_script;
    private Bottle1 spa_bottle;
    private Cuvette cuvettes;
    private Ball_disp ballDispenser;
    private CPLA_PREHEAT preheatCuvettesPlasma;
    private RHOL beads;
    private pipt reagentPipette;

    [SerializeField]
    private GameObject R_Panel_Text;
    [SerializeField]
    private GameObject small_ppt2;

    public int input_text_optn;
    public bool enter_key;
    public int MENU_NUM;
    public int ppt2_sle;
    public int tipchange1;

    public int protocolStep;

    [SerializeField]
    private TextMeshProUGUI R_Panel_Text_msg;
    [SerializeField]
    private TextMeshProUGUI R_Panel_lower_Text_msg;

    // CPLNX modifies this variable directly.
    public int CPLN = 0;
    private ScreenController screenController;
    private GameObject screenTile;
    void Start()
    {
        ppt3_script = FindObjectOfType<pipt3>();
        plasmaPipette = FindObjectOfType<SamplePipette>();
        preheat_hole_script = FindObjectOfType<SPA_Preheat>();
        ppt1_script = FindObjectOfType<pipt>();
        finalcube_script = FindObjectOfType<finalcube>();
        on_off_script = FindObjectOfType<ONOFF>();
        screenController = FindObjectOfType<ScreenController>();
        spa_bottle = FindObjectOfType<Bottle1>();
        cuvettes = FindObjectOfType<Cuvette>();
        ballDispenser = FindObjectOfType<Ball_disp>();
        preheatCuvettesPlasma = FindObjectOfType<CPLA_PREHEAT>();
        beads = FindObjectOfType<RHOL>();
        reagentPipette = FindObjectOfType<pipt>();

        screenTile = screenController.screenTile;
        ppt2_sle = 0;
        MENU_NUM = 0;
        input_text_optn = 0;
        protocolStep = -1;
    }

/*
    There's probably a better way to do the protocol check.
    Like running the function to check if criteria has been met each time something's interacted with.
*/
    void Update()
    {
        spa_pos = small_ppt2.transform.position;
        spaX_pos = small_ppt2.transform.position.x;
        // This is the protocol tutorial for Coagulation test PT-INR
        switch(protocolStep){
            case -1: // At the start of the protocol
                protocolStep = 0;
                R_Panel_Text_msg.text = "Switch on the instrument with the switch on the back.";
                R_Panel_lower_Text_msg.text = "Point at the switch and press hold E. You can't see the switch from here, so aim in the middle of the plastic part of the pipette of the Start4 instrument.";
            break;
            case 0: // At the start of the protocol
                if(on_off_script.system_on){ // The required action to trigger the next step in the protocol
                    protocolStep = 1; // Move to the next step
                    R_Panel_Text_msg.text = "Activate the Screen view by pointing towards the screen and pressing E";
                    R_Panel_lower_Text_msg.text = "Point at the screen and press hold E";
                }
            break;
            case 1:
                if(screenTile.activeSelf){ // Is the screenTile (UI) active? Means the user has followed the instructions.
                    protocolStep = 2; // Move to the next step
                    R_Panel_Text_msg.text = "Select Test Mode";
                    R_Panel_lower_Text_msg.text = "Point at N1 on instrument keyboard. Press and hold E. Then point at enter and press hold E.";
                }
            break;
            case 2:
                if(screenController.getMenuAddress == "/Test Mode"){ // Are we on the right address?
                    protocolStep = 3;
                    R_Panel_Text_msg.text = "Select PT from the menu";
                    R_Panel_lower_Text_msg.text = "Point at N1 on instrument keyboard. Press and hold E. Then point at enter and press hold E.";
                }
            break;
            case 3:
                if(screenController.getMenuAddress == "/Test Mode/PT"){
                    protocolStep = 5;
                    R_Panel_Text_msg.text = "Enter the patient ID";
                    R_Panel_lower_Text_msg.text = "Enter any number via the instrument keyboard. Then point at enter and press hold E.";
                }
            break;
            case 5:
                // Check if patient ID is entered
                if(screenController.patientID1 != 0){ // Are we on the right address?
                    protocolStep = 6;
                    R_Panel_Text_msg.text = "Finish entering the patient ID";
                    R_Panel_lower_Text_msg.text = "To enter another patient ID select another number. To finish entering the patient ID, point at enter and press hold E without a number selected.";
                }
            break;
            /* Menu states done. Below are physical procedures*/
            case 6:
                if(screenController.getMenuAddress == "/Test Mode/PT/Running Test"){
                    R_Panel_Text_msg.text = "Now we'll heat up the reagent in the step-pippette to 37 degrees. Select the preheat well.";
                    R_Panel_lower_Text_msg.text = "Point at the preheat well on the STart4 machine. Press hold E";
                    protocolStep = 7;
                }
            break;
            case 7:
                if(preheat_hole_script.preheat_selected){
                    R_Panel_Text_msg.text = "Set reagent to preheat. Move the reagent SPA+ bottle to preheating well.";
                    R_Panel_lower_Text_msg.text = "Point at SPA Bottle and press hold E";
                    protocolStep = 8;
                }
            break;
            // Set patient plasma to preheat in cuvettes.
            case 8:
                if(spa_bottle.bottle_preheat_set){ // Bottle has been set to preheat
                    R_Panel_Text_msg.text = "Place cuvettes in the incubation block.";
                    R_Panel_lower_Text_msg.text = "Point at the cuvette left of the machine. Press and hold E to select it";
                    protocolStep = 81;
                }
            break;
            case 81:
                if(cuvettes.cuvette_selected){ // Check if the cuvettes have been placed in the incubation block
                    R_Panel_Text_msg.text = "Place cuvettes in the incubation block.";
                    R_Panel_lower_Text_msg.text = "Now point at the leftmost slot for pre-heating. Press and hold E to move the cuvettes.";
                    protocolStep = 9;
                }
            break;
            case 9: // Add one bead to each cuvette
                if(cuvettes.inPosition){ // Check if the cuvettes have been placed in the incubation block
                    R_Panel_Text_msg.text = "Add the metal beads to the cuvettes before adding the diluted sample. This is to avoid splashing later.";
                    R_Panel_lower_Text_msg.text = "Point at the black round pipette at the top of the STart4 machine. Press hold E";
                    protocolStep = 10;
                }
            break;
            case 10: // Add diluted sample into cuvette
                if(ballDispenser.ballsDispensed){ // Have balls been dispensed?
                    R_Panel_Text_msg.text = "Pipette out the diluted sample (test & control) into the cuvettes.";
                    R_Panel_lower_Text_msg.text = "Select the small pipette. Then select the patient plasma. After each sample, change the tips by selecting the pipette, then the tip box. Do this 4 times.";
                    protocolStep = 11;
                }
            break;
            case 11: // Start the incubation clock
                if(plasmaPipette.pipetteCount == 4){
                    R_Panel_Text_msg.text = "Press the incubation button on the STart4 instrument to start the incubation clock.";
                    R_Panel_lower_Text_msg.text = "Point at the A button on the STart4 instrument and press hold E.";
                    protocolStep = 12;
                }
            break;   
            case 12: // Wait until the preheating of the samples are ready. The instrument will beep.
                if(preheatCuvettesPlasma.preheatStarted){
                    R_Panel_Text_msg.text = "Wait until the preheating of the samples are ready. The instrument will beep.";
                    R_Panel_lower_Text_msg.text = "Takes 3 seconds in this simulation. In real life 180 seconds approximately to heat to 37 degrees.";
                    protocolStep = 13;
                }
            break;
            case 13: // Fill the starting pipette with reagent and set the pipette
                if(preheatCuvettesPlasma.preheatDone){
                    R_Panel_Text_msg.text = "Move the cuvettes to the analyzer.";
                    R_Panel_lower_Text_msg.text = "Point at the last slot on the right. Press and hold E.";
                    protocolStep = 14;
                }
            break;

            case 14: // Preheating done. Move the cuvettes to the analyzer.
                if(cuvettes.inFinalPosition){
                    R_Panel_Text_msg.text = "Fill the starting pipette, set the pipette to 100 ul per pipette transfer.";
                    R_Panel_lower_Text_msg.text = "Point at the STart4 pipette. Press and hold E.";
                    protocolStep = 15;
                }
            break;

            // Start the INR test

            case 15: // Press the pipette button to start the test. Make sure the metal beads are moving
                if(reagentPipette.filled){ // Skipped as the animation is not ready
                    // Start showing the beads moving
                    R_Panel_Text_msg.text = "Press the pipette button to start the test. Make sure the metal beads are moving";
                    R_Panel_lower_Text_msg.text = "Point at the button under the cuvettes. Press E";
                    protocolStep = 16;
                }
            break;

            case 16: // Use the step pipette to pipette out reagent into the cuvettes. Start at the top and move your way down. The counter counts from top to bottom.
                if(beads.beadsMoving){
                    R_Panel_Text_msg.text = "Use the step pipette to pipette out reagent into the cuvettes. Start at the top and move your way down. The counter counts from top to bottom.";
                    R_Panel_lower_Text_msg.text = "Point at the cuvettes and hold E to begin.";
                    protocolStep = 17;
                }
            break;

            case 17: // Wait until the movement of the beads has stopped. The instrument will beep.
                if(reagentPipette.finished){
                    R_Panel_Text_msg.text = "Wait until the movement of the beads has stopped. The instrument will beep.";
                    R_Panel_lower_Text_msg.text = "Wait 10 seconds in this simulation.";
                    protocolStep = 18;
                }
            break;

            case 18: // Analysis is finished. The metal beads have stopped moving. See the report on the instrument receipt.
                if(!beads.beadsMoving){
                    R_Panel_Text_msg.text = "Analysis is finished. The metal beads have stopped moving. See the report on the instrument receipt.";
                    R_Panel_lower_Text_msg.text = "Point at the receipt. Press and hold E.";
                    protocolStep = 19;
                }
            break;
            case 19: // Show a report of the entire protocol (time taken each step, what steps were done etc.)
                    R_Panel_Text_msg.text = "The protocol is finished. You can now start a new protocol or do what you like in sandbox mode.";
                    R_Panel_lower_Text_msg.text = "Press escape to open the menu."; // Hide the lower text panel
                    // run a function to show the report
            break;
            default:
            break;
        }
    }
}
