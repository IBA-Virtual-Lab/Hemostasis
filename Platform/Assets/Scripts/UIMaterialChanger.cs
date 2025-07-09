using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterialChanger : MonoBehaviour
{
    public GameObject targetObject; // The object whose material will change
    private Renderer targetRenderer;

    public Material PTINRMaterial;
    public Material APTTMaterial;
    public Material liquidFibMaterial;

    void Start()
    {
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
        }
    }

    public void ChangeMaterial(string materialName)
    {
        if (targetRenderer != null)
        {
            switch (materialName)
            {
                case "PTINR":
                    targetRenderer.material = PTINRMaterial;
                    Debug.Log("ptinr rendered");
                    break;
                case "APTT":
                    targetRenderer.material = APTTMaterial;
                    Debug.Log("aptt rendered");
                    break;
                case "LiquidFib":
                    targetRenderer.material = liquidFibMaterial;
                    Debug.Log("liquidFib rendered");
                    break;
                default:
                    Debug.LogWarning("Material not found");
                    break;
            }
        }
    }
}