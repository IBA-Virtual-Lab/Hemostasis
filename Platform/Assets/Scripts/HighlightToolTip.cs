using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HighlightToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color highlightColor = Color.white;
    private List<Material> materials;
    public bool isEnable = true;

    public string tooltipMessage;

    void Start()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }

        //if (ToolTipManager._instance != null)
        //{
        //    ToolTipManager._instance.gameObject.SetActive(false); // Deactivate the Canvas initially
        //}
    }

    void OnMouseOver()
    {
        //if (!isCanvasActive)
        //{
        //    HighlightObject();
        //    ToolTipManager._instance.SetAndShowToolTip(tooltipMessage);
        //}
        if (isEnable) {
            HighlightObject();
        }
    }

    void OnMouseExit()
    {
        if (isEnable) {
            UnhighlightObject();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // This function is called when the mouse enters the UI element
        //Debug.Log("Mouse entered the object!");
        if (isEnable)
        {
            HighlightObject();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // This function is called when the mouse exits the UI element
        //Debug.Log("Mouse exited the object!");
        if (isEnable)
        {
            UnhighlightObject();
        }
    }

    public void HighlightObject()
    {
        // add emission to game object
        //foreach (var material in materials)
        //{
        //    material.EnableKeyword("_EMISSION");
        //    material.SetColor("_EmissionColor", highlightColor);
        //}
        // enable tool tip
        ToolTipManager._instance.SetAndShowToolTip(tooltipMessage);
        //Debug.Log("Highlight Called:" + tooltipMessage);
    }

    public void UnhighlightObject()
    {
        // disable emission to game object
        //foreach (var material in materials)
        //{
        //    material.DisableKeyword("_EMISSION");
        //}
        // disable tool tip
        ToolTipManager._instance.HideToolTip();
        //Debug.Log("Unhighlight Called");
    }

    public void SetEnable(bool enable) {
        isEnable = enable;
    }
}
