using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ProgressBar : MonoBehaviour
{

    [SerializeField] public Slider slider;
    public Animator animator;
    //public List<Animator> animators;
    [SerializeField] string[] animationTriggers; // Array of trigger names from animator
    public List<GameObject> activators;  // Assign the animator object in the inspector
    public List<GameObject> deactivators;  // Assign the animator object in the inspector
    private float stepSize;
    [SerializeField] public float triggerValue = 10; // Set this to the value at which the animation should play

    // Set the maximum number of steps
    public int maxSteps = 22;

    // Initialize the progress bar
    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        slider.maxValue = maxSteps;
        slider.value = 0;
        stepSize = 1f / (maxSteps - 1);
    }

    // Update the progress bar based on the number of completed steps
    public void AdvanceProgress()
    {
        // Optionally, disable interaction while setting the value
        slider.interactable = false;
        slider.value = slider.value + 1;
        // Optionally, re-enable interaction after setting the value
        slider.interactable = true;
    }

    public void ReverseProgress()
    {
        // Optionally, disable interaction while setting the value
        slider.interactable = false;
        slider.value = slider.value - 1;
        // Optionally, re-enable interaction after setting the value
        slider.interactable = true;
    }

    public void SetProgress(int step)
    {
        // Optionally, disable interaction while setting the value
        slider.interactable = false;
        slider.value = step;
        // Optionally, re-enable interaction after setting the value
        slider.interactable = true;
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    // This is called every time the handle is dragged
    //    Debug.Log("Dragging");
    //    if (slider.value >= triggerValue)
    //    {
    //        // Trigger the animation if the handle is dragged past the trigger value
    //        //animator.SetTrigger("PlayAnimation");
    //        Debug.Log("Alternative trigger method: slider > triggerValue");
    //    }
    //}

    // Interface method for beginning drag - required by IBeginDragHandler
    public void BeginDrag(PointerEventData eventData)
    {
        // This code is executed when a drag starts.
    }

    public void EndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        // Get the current value of the slider when the drag ends
        float currentValue = slider.value;

        // Call OnSliderChanged to process the current value
        //OnSliderChanged(currentValue);

    }

    //public void OnSliderChanged(float currentValue)
    //{
    //    // Quantize the slider value to the nearest step
    //    int stepIndex = Mathf.FloorToInt(slider.value / stepSize);
    //    Debug.Log("stepIndex at Drag Ended:" + stepIndex);

    //    // Trigger the corresponding animation based on the step index
    //    switch (stepIndex)
    //    {
    //        case 0:
    //            Debug.Log("Case 0");
    //            //animator.SetTrigger("Anim1");
    //            break;
    //        case 1:
    //            Debug.Log("Case 1");
    //            //animator.SetTrigger("Anim2");
    //            break;
    //        case 2:
    //            Debug.Log("Case 2");
    //            //animator.SetTrigger("Anim3");
    //            break;
    //        case 3:
    //            Debug.Log("Case 3");
    //            //animator.SetTrigger("Anim4");
    //            break;
    //        case 4:
    //            Debug.Log("Case 4");
    //            //animator.SetTrigger("Anim5");
    //            break;
    //            // Add more cases as needed for additional steps
    //    }
    //}
}