using UnityEngine;
using TMPro;

public class StepNamer : MonoBehaviour
{
    private TMP_Text textMesh;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        if (textMesh != null && transform.parent != null)
        {
            textMesh.text = transform.parent.name;
        }
        else
        {
            Debug.LogError("This script requires a TextMeshPro component and a parent object!");
        }
    }
}
