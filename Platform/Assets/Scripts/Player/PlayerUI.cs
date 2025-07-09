using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    This script is mounted to the Player object.
    It requires the PlayerUI object to be bound to it via the inspector.
*/

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

}
