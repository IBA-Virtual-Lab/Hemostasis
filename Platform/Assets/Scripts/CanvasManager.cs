using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{
    void Start()
    {
        // Add this canvas to the newSceneCanvases list in the GameManager
        GameManager.NewSceneCanvases.Add(GetComponent<Canvas>());
    }
}
