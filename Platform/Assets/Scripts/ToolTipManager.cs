using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager _instance;

    [SerializeField] public TextMeshProUGUI textComponent1;
    [SerializeField] public TextMeshProUGUI textComponent2;
    [SerializeField] public TextMeshProUGUI textComponent3;

    [SerializeField] public Canvas canvas1;
    [SerializeField] public Canvas canvas2;
    [SerializeField] public Canvas canvas3;

    [SerializeField] public GameObject tooltippanel1;
    [SerializeField] public GameObject tooltippanel2;
    [SerializeField] public GameObject tooltippanel3;

    [SerializeField] public RectTransform panelRectTransform1; // Assign the panel's RectTransform in inspector
    [SerializeField] public RectTransform panelRectTransform2; // Assign the panel's RectTransform in inspector
    [SerializeField] public RectTransform panelRectTransform3; // Assign the panel's RectTransform in inspector

    public Vector2 storedCursorPos;

    private GameObject tooltipPanel;
    private RectTransform panelRectTransform;
    private TextMeshProUGUI textComponent;
    private Color highlightColor = Color.white;
    private Canvas canvas;

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(true);
        tooltipPanel = tooltippanel1;
        panelRectTransform = panelRectTransform1;
        textComponent = textComponent1;
        //Debug.Log("Awakened!");
    }

    void Start()
    {
        Cursor.visible = true;
        tooltipPanel.SetActive(false);

        if (gameObject.activeSelf == false) // Check if already active
        {
            gameObject.SetActive(true);
            //Debug.Log("Started!");
        }
    }

    void Update()

    {
        if (canvas1.gameObject.activeInHierarchy)
        {
            tooltipPanel = tooltippanel1;
            panelRectTransform = panelRectTransform1;
            textComponent = textComponent1;
            canvas = canvas1;
        }
        
        if (canvas2.gameObject.activeInHierarchy)
        {
            tooltipPanel = tooltippanel2;
            panelRectTransform = panelRectTransform2;
            textComponent = textComponent2;
            canvas = canvas2;
        }
        if (canvas3.gameObject.activeInHierarchy)
        {
            tooltipPanel = tooltippanel3;
            panelRectTransform = panelRectTransform3;
            textComponent = textComponent3;
            canvas = canvas3;
        }   
        
        storedCursorPos.x = Input.mousePosition.x + 1;
        storedCursorPos.y = Input.mousePosition.y + 1;
        //Debug.Log("mouse position on update:" + Input.mousePosition);
    }

    // Modify the method to take the message and a reference to the tooltipCanvas
    public void SetAndShowToolTip(string message)
    {
        // Debug.Log("Tooltip Shown");
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPos.z = 0; // Assuming you want the panel on the XZ plane (adjust Z as needed)
        panelRectTransform.anchoredPosition = storedCursorPos;
        //Debug.Log("Mouse position:" + panelRectTransform.anchoredPosition);
        tooltipPanel.SetActive(true);
        textComponent.text = message;
        Debug.Log("Set and Shown:" + message + " for " + tooltipPanel + " at " + panelRectTransform.anchoredPosition + " because of " + canvas);
    }

    // Modify the method to take a reference to the tooltipCanvas
    public void HideToolTip()
    {
        // Debug.Log("Tooltip Hidden");
        tooltipPanel.SetActive(false);
        textComponent.text = string.Empty;
        //Debug.Log("Unhighlight Called");
    }

    //// Call this method when you want to close the modal panel
    //public void CloseModalPanel()
    //{
    //    if (OnCloseModalPanel != null)
    //    {
    //        OnCloseModalPanel();
    //    }
    //}

    //private void HighlightObject(List<Material> materials)
    //{
    //    foreach (var material in materials)
    //    {
    //        material.EnableKeyword("_EMISSION");
    //        material.SetColor("_EmissionColor", highlightColor);
    //        Debug.Log($"Material: {material.name}, Original Color: {material.color}, Emission Color: {highlightColor}");
    //    }

    //    Debug.Log("HighlightObject method finished.");
    //}

    //// Public method that calls the private HighlightObject method
    //public void CallHighlightObject(List<Material> materialsToHighlight)
    //{
    //    HighlightObject(materialsToHighlight);
    //}
}
