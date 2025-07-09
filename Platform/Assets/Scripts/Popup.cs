using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI text; // Use TextMeshProUGUI if using TextMeshPro
    public float displayTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to display the text
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()

    {

        text.gameObject.SetActive(true); // Show the text
        yield return new WaitForSeconds(displayTime); // Wait for the specified time
        text.gameObject.SetActive(false); // Hide the text

    }

}