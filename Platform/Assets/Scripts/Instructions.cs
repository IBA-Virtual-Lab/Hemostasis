using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Instructions : MonoBehaviour
{
    public List<Animator> animators;  // Assign the animator object in the inspector
    public List<GameObject> activators;  // Assign the animator object in the inspector
    public List<GameObject> deactivators;  // Assign the animator object in the inspector
    [SerializeField] string[] animationTriggers; // Array of trigger names from animator

    [SerializeField] GameObject previousPanel; // New panel to activate
    [SerializeField] GameObject currentPanel; // Current panel to deactivate
    [SerializeField] GameObject newPanel; // New panel to activate
    [SerializeField] GameObject checkQuitPanel; // Panel to activate when exit clicked

    [SerializeField] private GameObject nextButton; // Reference to the button to activate

    private GameObject progressBarObject;

    private void Start()
    {
        progressBarObject = GameObject.Find("Progress");
    }

    public void First(Action callback)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            if (i < animationTriggers.Length)
            {
                animators[i].SetTrigger(animationTriggers[i]);
            }
        }


                foreach (Animator animator in animators)
        {
            foreach (string trigger in animationTriggers)
            {
                animator.SetTrigger(trigger);
                Debug.Log("Triggered " + trigger);
            }
        }
        callback?.Invoke();
    }

    public void TriggerActions()
    {
        Debug.Log("Click on trigger detected");
        for (int i = 0; i < animators.Count; i++)
        {
            if (i < animationTriggers.Length)
            {
                animators[i].SetTrigger(animationTriggers[i]);
                Debug.Log("Triggered " + animationTriggers[i]);
            }
        }

        foreach (GameObject activator in activators)
        {
            activator.SetActive(true);
        }

        foreach (GameObject deactivator in deactivators)
        {
            deactivator.SetActive(false);
        }

        RevealNext();
        //if (nextButton = null)
        //{
        //    Next();
        //}

    }

    public void ActivateQuitCheck()
    {
        if (checkQuitPanel != null)
        {
            checkQuitPanel.SetActive(true);
        }
        else
        {
            Debug.Log("No panel assigned to checkQuitPanel in ActivateQuickCheck!");
        }
    }

    public void DeactivateQuitCheck()
    {
        if (checkQuitPanel != null)
        {
            checkQuitPanel.SetActive(false);
        }
        else
        {
            Debug.Log("No panel assigned to checkQuitPanel in Instructions");
        }
    }

    public void RevealNext()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(true);
        }
    }

    public void Back()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        if (previousPanel != null)
        {
            previousPanel.SetActive(true);
            if (progressBarObject != null)
            {
                progressBarObject.GetComponent<ProgressBar>().ReverseProgress();
            }
            else
            {
                Debug.Log("ProgressBar object not found!");
            }
        }

    }

    public void Next()
    {
        foreach (GameObject activator in activators)
        {
            activator.SetActive(false);
        }

        foreach (GameObject deactivator in deactivators)
        {
            deactivator.SetActive(true);
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        if (newPanel != null)
        {
            newPanel.SetActive(true);
            if (progressBarObject != null)
            {
                progressBarObject.GetComponent<ProgressBar>().AdvanceProgress();
            }
            else
            {
                Debug.Log("ProgressBar object not found!");
            }
        }

    }
}
