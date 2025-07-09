using UnityEngine;

// Example usage in another script
public class FillPipette : MonoBehaviour
{
    public PipetteAttribs Large;

    void Update()
    {
        // Example: Fill the bottle when the player presses a key.
        if (Input.GetKeyDown(KeyCode.F))
        {
            Large.Fill(10.0f); // Fill the bottle by 10 units.
        }

        // Example: Empty the bottle when the player presses another key.
        if (Input.GetKeyDown(KeyCode.E))
        {
            Large.Empty(5.0f); // Empty the bottle by 5 units.
        }
    }
}
