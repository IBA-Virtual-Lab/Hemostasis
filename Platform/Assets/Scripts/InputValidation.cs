// InputValidationScript.cs
using UnityEngine;
using UnityEngine.UI;

public class InputValidationScript : MonoBehaviour
{
    public InputField inputField;
    public Text errorMessageText;

    // Function to be called when the user presses a button to submit the input
    public void SubmitInput()
    {
        string userInput = inputField.text;

        // Perform your validation logic here
        if (IsValidInput(userInput))
        {
            errorMessageText.text = string.Empty; // Clear error message if input is valid
            // Proceed with further actions for valid input
        }
        else
        {
            errorMessageText.text = "Invalid input. Please try again."; // Show error message
        }
    }

    // Example validation logic
    private bool IsValidInput(string input)
    {
        // Implement your validation criteria here
        // For example, you can check if the input is a number
        return int.TryParse(input, out _);
    }
}
