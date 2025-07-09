using UnityEngine;
using UnityEngine.UI;

public class ConditionalAdvance : MonoBehaviour
{
    [SerializeField] private Button button; // Reference to the button to activate
    public Animator animator; // Public Animator field

    private bool animationFinished;

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("No Animator component assigned!");
        }

        if (button == null)
        {
            Debug.LogError("No Button component found!");
        }
    }

    private void Update()
    {
        if (!animationFinished && animator != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animationFinished = true;
            button.interactable = true;
        }
    }
}
