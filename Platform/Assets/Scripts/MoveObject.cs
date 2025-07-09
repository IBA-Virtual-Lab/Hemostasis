using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool moveable = true; // Option to rotate the object when selected
    //public bool rotateUpWhenSelected = false; // Option to rotate the object when selected
    //public bool rotateleftWhenSelected = false; // Option to rotate the object when selected



    private bool isDragging = false;
    private GameObject lastCollidedObject;
    private Vector3 originalPosition;
    private Animator animator; // Reference to the Animator component
    private Quaternion originalRotation;
    private Vector3 mOffset;
    private float mZCoord;

    private HighlightToolTip[] AllObjectScripts;

    // Create a dictionary to map animation names to their corresponding triggers
    private Dictionary<string, string> animationTriggers = new Dictionary<string, string>();

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        animator = GetComponent<Animator>(); // Get the Animator component

        // Find all objects with HighlightToolTip in the scene
        AllObjectScripts = GameObject.FindObjectsOfType<HighlightToolTip>();
        //PopulateAnimationDictionary();
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        if (moveable == true)
        {
            isDragging = true;
            // Disable the Animator while dragging
            if (animator != null)
            {
                animator.enabled = false;
            }

            SetEnableOtherGameObjects(false);
        }
        
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Cursor.visible = true; // Show the mouse cursor when not dragging

            // Update the position of the GameObject based on the mouse position
            transform.position = GetMouseAsWorldPoint() + mOffset;


            //// Rotate and lift the object when selected
            //if (rotateUpWhenSelected && !rotationApplied)
            //{
            //    // Apply the rotation in the Y-Z plane
            //    transform.Rotate(Vector3.right, -90f);

            //    // Lift the object
            //    // transform.position += Vector3.up * liftAmount;

            //    // Set the rotation flag to true to avoid continuous rotation
            //    rotationApplied = true;
            //}
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (animator != null)
        {
            animator.enabled = true;
        }
        Cursor.visible = true; // Show the mouse cursor when not dragging

        if (lastCollidedObject != null)
        {
            // Trigger your event or do something else when both OnMouseUp and collision occur
            transform.position = originalPosition;
            //Debug.Log("OnMouseUp and collision occurred with: " + lastCollidedObject.name);
            // Print the dictionary to the console
            PrintDictionary(animationTriggers);
            //PlayAnimation(lastCollidedObject.name);
            lastCollidedObject = null;
        }
        else
        {
            // If you want the object to return to its original position when OnMouseUp and no collision
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            //Debug.Log("No collision occurred!");
        }

        SetEnableOtherGameObjects(true);  // enable highligh and tooltip of other objects
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    // Check if the dragged object triggers a collision with another object by name
    //    if (isDragging)
    //    {
    //        lastCollidedObject = other.gameObject;
    //        if (lastCollidedObject.GetComponent<HighlightToolTip>() != null) 
    //        {
    //            lastCollidedObject.GetComponent<HighlightToolTip>().HighlightObject();
    //        }
    //        // Debug.Log("Collision detected with: " + other.gameObject.name);
    //        // Trigger your event or do something else when a collision occurs
    //    }
    //    else
    //    {
    //        lastCollidedObject = null;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (isDragging)
    //    {
    //        if (lastCollidedObject.GetComponent<HighlightToolTip>() != null) 
    //        {
    //            lastCollidedObject.GetComponent<HighlightToolTip>().UnhighlightObject();
    //        }
    //        else
    //        {
    //            Debug.Log("No tooltip on collided object");
    //        }
    //        lastCollidedObject = null;
    //        // Debug.Log("Exited collision with: " + other.gameObject.name);
    //    }
    //}

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void SetEnableOtherGameObjects(bool isEnable)
    {
        foreach (HighlightToolTip ObjectScripts in AllObjectScripts)
        {
            // You can now access each highlightScript and the corresponding GameObject
            // GameObject highlightedObject = highlightScript.gameObject;

            // Perform any operations with the highlightedObject or highlightScript
            // For example, you might want to disable highlighting for all objects at the start
            ObjectScripts.SetEnable(isEnable);
        }
    }

    private void HandleDragAndRotate()
    {
        // Rotate and lift the object when selected
        // transform.position += Vector3.up * liftAmount;
        //transform.rotation *= Quaternion.Euler(-90, 0, 0);
        transform.position += Vector3.up * 100.1f;
    }

    // Method to play an animation based on its name
    //void PlayAnimation(string animationName)
    //{
    //    // Check if the animationName exists in the dictionary
    //    if (animationTriggers.ContainsKey(animationName))
    //    {
    //        // Get the trigger associated with the animationName
    //        string trigger = animationTriggers[animationName];
    //        Debug.Log(trigger);

    //        // Trigger the animation using the Animator component
    //        animator.SetTrigger(trigger);
    //    }
    //    else
    //    {
    //        Debug.Log("Animation not found in the dictionary: " + animationName);
    //    }
    //}

    // Method to populate the animation dictionary based on the name of the GameObject
    //void PopulateAnimationDictionary()
    //{
    //    // Get the name of the GameObject
    //    string objectName = gameObject.name;
    //    Debug.Log("Dictionary generated from:" + objectName);

    //    // Use the name to determine which dictionary to populate
    //    if (objectName == "PipetteLarge")
    //    {
    //        PipetteLargeDictionary();
    //        Debug.Log("PipetteLargeDictionary chosen");
    //    }
    //    else if (objectName == "PipetteSmall")
    //    {
    //        PipetteSmallDictionary();
    //        Debug.Log("PipetteSmallDictionary chosen");
    //    }
    //    else if (objectName == "Cuvette")
    //    {
    //        CuvetteDictionary();
    //        Debug.Log("CuvetteDictionary chosen");
    //    }
    //    else if (objectName == "Start4_Pipette")
    //    {
    //        Start4_PipetteDictionary();
    //        Debug.Log("Start4_PipetteDictionary chosen");
    //    }
    //    else if (objectName == "Reagent1")
    //    {
    //        Reagent1Dictionary();
    //        Debug.Log("Reagent1Dictionary chosen");
    //    }
    //    else if (objectName == "Bead_Dispenser")
    //    {
    //        BeadDispenserDictionary();
    //        Debug.Log("BeadDispenserDictionary chosen");
    //    }
    //    else
    //    {
    //        Debug.Log("No specific dictionary found for GameObject: " + objectName);
    //    }
    //}

    // Methods to populate the animation dictionaries based on the object selected
    void PipetteLargeDictionary()
    {
        // Add entries to the dictionary (replace these with your actual animation names and triggers)
        //This dictionary is for the PipetteLarge object
        animationTriggers.Add("Buffer", "take_ob1");
        // Large pipette extract from buffer, deposit to tube 2
        animationTriggers.Add("PipetteTipDisposalBox", "disp_tip");
        // Large pipette tip disposal (no replacement)
        animationTriggers.Add("PipetteTipBox2", "get_tip");
        // Large pipette add tip (no dispose)
        animationTriggers.Add("PipetteTipBox", "change_tip");
        // Large pipette tip replacement
        animationTriggers.Add("TubePatientPlasma2", "cuvt_fill");
        // Large pipette extract from Tube 2, transfer to cuvette on machine
    }

    void CuvetteDictionary()
    {
        //animationTriggers.Add("STart4", "incubate_cuvette");
        // Move cuvette to incubation block
        animationTriggers.Add("Measurement", "measure");
        // Move cuvette to measuring position
    }

    void Start4_PipetteDictionary()
    {
        animationTriggers.Add("Reagent1", "preheat_on");
        animationTriggers.Add("Buffer", "take_ob1");
        // Large pipette extract from buffer, deposit to tube 2
        animationTriggers.Add("PipetteTipDisposalBox", "disp_tip");
        // Large pipette tip disposal (no replacement)
        animationTriggers.Add("PipetteTipBox2", "get_tip");
        // Large pipette add tip (no dispose)
        animationTriggers.Add("PipetteTipBox", "change_tip");
        // Large pipette tip replacement
        animationTriggers.Add("TubePatientPlasma2", "cuvt_fill");
        // Large pipette extract from Tube 2, transfer to cuvette on machine
        // Start4_Pipette extracts from heated reagent to cuvette on machine
    }

    void Reagent1Dictionary()
    {
        animationTriggers.Add("STart4", "preheat_on");
        // Move reagent1 to heater on Start4
    }

    void BeadDispenserDictionary()
    {
        animationTriggers.Add("STart4", "dispense_beads");
        // Add beads to cuvette for stirring
    }

    void PipetteSmallDictionary()
    {
        animationTriggers.Add("PipetteTipDisposalBox", "change_2");
        // Small pipette tip change
        animationTriggers.Add("TubePatientPlasma1", "tgr_ppt1");
        // Small pipette extract tube 1, depost cuvette on machine
    }

    void PrintDictionary(Dictionary<string, string> dictionary)
    {
        foreach (var kvp in dictionary)
        {
            Debug.Log("Key: " + kvp.Key + ", Value: " + kvp.Value);
        }
    }
}
