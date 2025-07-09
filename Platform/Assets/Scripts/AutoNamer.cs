using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoNamer : MonoBehaviour
{
    public string spacer = "\t";
    // Start is called before the first frame update
    private void Start()
    {
        // Get the TextMeshPro component
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        // Set the text to the button's name
        if (textMeshPro != null)
        {
            textMeshPro.text = spacer + gameObject.name;
        }
    }
}
