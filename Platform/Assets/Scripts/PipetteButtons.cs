using UnityEngine;
using UnityEngine.UI;

public class PipetteButtons : MonoBehaviour
{
    private Animator animator;
    public GameObject panelToDeactivate;

    void Start()
    {
        // Get the Animator component from the parent GameObject
        animator = transform.parent.GetComponent<Animator>();

    }

    public void OnButtonClick1()
    {
        // Trigger the first animation
        animator.SetTrigger("change_tipp");

        // Deactivate the panel
        DeactivatePanel();
    }

    public void OnButtonClick2()
    {
        // Trigger the second animation
        animator.SetTrigger("take_ob1");

        // Deactivate the panel
        DeactivatePanel();
    }

    public void OnButtonClick3()
    {
        // Trigger the second animation
        animator.SetTrigger("cuvt_fill");

        // Deactivate the panel
        DeactivatePanel();
    }

    public void OnCloseButtonClick()
    {
        // Close the modal panel
        DeactivatePanel();
    }

    // Add more methods for additional buttons if needed

    private void DeactivatePanel()
    {
        // Deactivate the panel
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }
    }

}
