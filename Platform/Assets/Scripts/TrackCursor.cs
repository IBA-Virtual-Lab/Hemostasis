//using UnityEngine;
//using UnityEngine.EventSystems;

//public class TrackCursor : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
//{
//    private RectTransform rectTransform;
//    private Canvas canvas;
//    private CanvasGroup canvasGroup;

//    private bool isDragging = false;
//    private Vector2 offset;

//    void Start()
//    {
//        // Get necessary components.
//        rectTransform = GetComponent<RectTransform>();
//        canvas = GetComponentInParent<Canvas>();
//        canvasGroup = GetComponent<CanvasGroup>();
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        // Record the offset between the object's position and the mouse position.
//        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);

//        // Set the object as being dragged.
//        isDragging = true;

//        // Make the object appear on top of other UI elements.
//        canvasGroup.blocksRaycasts = false;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        if (isDragging)
//        {
//            // Calculate the new position based on the mouse position and the initial offset.
//            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 newPosition);
//            rectTransform.anchoredPosition = newPosition - offset;
//        }
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        // Stop dragging the object.
//        isDragging = false;

//        // Allow the object to interact with other UI elements.
//        canvasGroup.blocksRaycasts = true;
//    }
//}
