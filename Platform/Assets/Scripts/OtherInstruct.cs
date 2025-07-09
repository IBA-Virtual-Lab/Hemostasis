using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherInstruct : MonoBehaviour
{
    [SerializeField] Button button; // Assign the button object in the inspector
    public List<Animator> animators;  // Assign the animator object in the inspector
    public List<GameObject> activators;  // Assign the animator object in the inspector
    [SerializeField] string[] animationTriggers; // Array of trigger names from animator

    [SerializeField] GameObject currentPanel; // Current panel to deactivate
    [SerializeField] GameObject newPanel; // New panel to activate

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(TriggerActions);
        }
    }

    public void TriggerActions()
    {
        Application.Quit();
        foreach (Animator animator in animators)
        {
            foreach (string trigger in animationTriggers)
            {
                animator.SetTrigger(trigger);
            }
        }

        foreach (GameObject activator in activators)
        {
            activator.SetActive(true);
        }

        // Deactivate the panel
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        if (newPanel != null)
        {
            newPanel.SetActive(true);
        }

    }
}
