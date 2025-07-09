using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StepButtons : MonoBehaviour
{
    // List of panel names to exclude from deactivation
    public List<string> panelsToExclude = new List<string> { "Progress" };
    [SerializeField] GameObject newPanel; // New panel to activate
    public List<Animator> animators;  // Assign the animator object in the inspector
    public List<GameObject> activators;  // Assign the animator object in the inspector
    public List<GameObject> deactivators;  // Assign the animator object in the inspector
    [SerializeField] string[] animationTriggers; // Array of trigger names from animator
    public int steporder;  // Order in sequence of steps
    private GameObject progressBarObject;

    private void Start()
    {
        progressBarObject = GameObject.Find("Progress");
    }

    // Call this method when the button is clicked
    public void DeactivateActivePanels()
    {
        Debug.Log("Called Step Button");
        // Find all GameObjects with the tag 'Panel' that are currently active in the scene
        GameObject[] allPanels = GameObject.FindGameObjectsWithTag("Respawn");
        List<GameObject> activePanels = new List<GameObject>();

        foreach (GameObject panel in allPanels)
        {
            if (panel.activeSelf && !panelsToExclude.Contains(panel.name))
            {
                activePanels.Add(panel);
                //Debug.Log("Active panel detected: " + panel);
            }
        }


        // Deactivate all active panels except the ones to exclude
        foreach (GameObject activePanel in activePanels)
        {
            activePanel.SetActive(false);
            //Debug.Log("Set inactive:" + activePanel);
        }

        if (newPanel != null)
        {
            newPanel.SetActive(true);
            Debug.Log("Set active:" + newPanel);
        }

        if (progressBarObject != null)
        {
            progressBarObject.GetComponent<ProgressBar>().SetProgress(steporder);
        }
        else
        {
            //Debug.Log("ProgressBar object not found!");
        }

        // Highlight the button
        EventSystem.current.SetSelectedGameObject(this.gameObject);

        // Set each animator only with its corresponding trigger, only if not already in the desired state
        TriggerAnimations();

    }

    // Animator: list[i] from animators; new_trigger: animationTriggers[i], gameObject: object whose state corresponds to 
    void TestState(Animator current, string new_trigger, string gameObject, string state, string trigger_word)
    {
        //Debug.Log("Check on animator for " + current + " against " + gameObject);

        Debug.Log("Current game object name: " + current.gameObject.name + ", vs reference name " + gameObject);

        if (current.gameObject.name == gameObject)
        {
            Debug.Log(current.gameObject.name + " Gets inside");
            //AnimatorStateInfo stateInfo = current.GetCurrentAnimatorStateInfo(0);
            //int currentStateHash = stateInfo.fullPathHash;
            //string currentStateName = Animator.StringToHash(currentStateHash);
            //Debug.Log("Compare states: from loop " + currentStateName + " vs. text in " + state);
            if (current.GetCurrentAnimatorStateInfo(0).IsName(state))
            {
                Debug.Log("New_trigger: " + new_trigger + "; Trigger-word: " + trigger_word);
                if (new_trigger != trigger_word)
                {
                    current.SetTrigger(new_trigger);
                    Debug.Log("Triggered animator for " + current + " with animationTrigger " + new_trigger + " from state " + state);
                    
                }
                else
                {
                    Debug.Log(current + " animator is already in the state" + state);
                }
            }
            else
            {
                Debug.Log("But not inside " + state);
            }
        }
        else
        {
            Debug.Log(current.gameObject.name + " != " + gameObject);
        }
    }

    void TriggerAnimations()
    {
        for (int i = 0; i < animators.Count; i++)
        {
            if (i < animationTriggers.Length)
            {
                // ..."Object", "state", "trigger");
                TestState(animators[i], animationTriggers[i], "Cuvette", "resting_cuvette", "jump_rest");
                TestState(animators[i], animationTriggers[i], "Cuvette", "whileIncubating", "jump_incub");
                TestState(animators[i], animationTriggers[i], "Cuvette", "whileIncubatingFull", "jump_full");
                TestState(animators[i], animationTriggers[i], "Cuvette", "measuring", "jump_meas");
                TestState(animators[i], animationTriggers[i], "Cuvette", "measuring", "jump_meas");

                TestState(animators[i], animationTriggers[i], "Reagent1", "reag1_rests", "jump_reag1rests");
                TestState(animators[i], animationTriggers[i], "Reagent1", "reag1_incubing", "jump_reag1incubing");

                TestState(animators[i], animationTriggers[i], "Reagent2_CaCl", "reag1_rests", "jump_reag1rests");
                TestState(animators[i], animationTriggers[i], "Reagent2_CaCl", "reag1_incubing", "jump_reag1incubing");

                TestState(animators[i], animationTriggers[i], "Reagent4_LiquidFib", "reag1_rests", "jump_reag1rests");
                TestState(animators[i], animationTriggers[i], "Reagent4_LiquidFib", "reag1_incubing", "jump_reag1incubing");

                TestState(animators[i], animationTriggers[i], "Camera", "static_camera", "jump_static");
                TestState(animators[i], animationTriggers[i], "Camera", "STart_zoom", "jump_zoomed");

                Debug.Log("Current iteration: " + i + ", current animator " + animators[i] + ", current animationTrigger " + animationTriggers[i]);

                TestState(animators[i], animationTriggers[i], "Beads", "no_beads", "beads_gone");
                TestState(animators[i], animationTriggers[i], "Beads", "yes_beads", "beads_off");
                TestState(animators[i], animationTriggers[i], "Beads", "stir_beads", "beads_on");

                TestState(animators[i], animationTriggers[i], "TubeforDilution", "stationary", "toempty");
                TestState(animators[i], animationTriggers[i], "TubeforDilution", "full", "tofull");

                TestState(animators[i], animationTriggers[i], "Start4_Pipette", "resting_StPip", "empty");
                TestState(animators[i], animationTriggers[i], "Start4_Pipette", "full_restingStppt", "filled");
            }
        }
    }
}
