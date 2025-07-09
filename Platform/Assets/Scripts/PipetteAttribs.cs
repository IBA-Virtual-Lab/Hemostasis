using UnityEngine;
using TMPro;
using System;


public class PipetteAttribs : MonoBehaviour
{
    [SerializeField]
    private float maxVolume = 100.0f; // Maximum volume of the bottle.

    private float volume = 0.0f;
    private string substance = "Empty"; // Default substance.

    private bool isMouseOver = false;

    public TextMeshProUGUI fillLevelText; // Reference to the TextMeshProUGUI component.
    public GameObject fillLevelPanel; // Reference to the panel containing the fill level.

    void Start()
    {
        UpdateFillLevelText();
        SetFillLevelPanelActive(false); // Start with the panel deactivated.
    }

    void Update()
    {
        // Check if the mouse is over the collider of the bottle.
        if (isMouseOver)
        {
            // You can perform additional actions when the mouse is over the bottle.
            // For example, display a tooltip with information about the substance.
        }
    }

    public void Fill(float amount)
    {
        volume += amount;
        volume = Mathf.Clamp(volume, 0f, maxVolume); // Clamp the volume within the specified range.
        UpdateFillLevelText();
        Debug.Log("Bottle filled. Current volume: " + volume);
    }

    void UpdateSubstance(string newSubstance)
    {
        // Use the newSubstance value as needed, e.g., update the substance in PipetteAttribs.
        SetSubstance(newSubstance);
    }

    public void Empty(float amount)
    {
        volume -= amount;
        volume = Mathf.Max(volume, 0f); // Ensure the volume doesn't go below zero.
        UpdateFillLevelText();
        Debug.Log("Bottle emptied. Current volume: " + volume);
    }

    public void SetSubstance(string newSubstance)
    {
        substance = newSubstance;
        UpdateFillLevelText();
        Debug.Log("Substance set to: " + substance);
    }

    private void UpdateFillLevelText()
    {
        // Update the TextMeshProUGUI text to display the current fill level and substance.
        if (fillLevelText != null)
        {
            fillLevelText.text = "Fill Level: " + volume.ToString("F1") + " / " + maxVolume + "\nSubstance: " + substance;
        }
    }

    private void SetFillLevelPanelActive(bool isActive)
    {
        // Activate or deactivate the fill level panel.
        if (fillLevelPanel != null)
        {
            fillLevelPanel.SetActive(isActive);
        }
    }

    // Called when the mouse enters the collider of the bottle.
    void OnMouseEnter()
    {
        isMouseOver = true;
        SetFillLevelPanelActive(true); // Activate the fill level panel.
    }

    // Called when the mouse exits the collider of the bottle.
    void OnMouseExit()
    {
        isMouseOver = false;
        SetFillLevelPanelActive(false); // Deactivate the fill level panel.
    }

    public float CurrentVolume
    {
        get { return volume; }
    }

    public string CurrentSubstance
    {
        get { return substance; }
    }
}
