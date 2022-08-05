//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

///// <summary>
///// This will handle drag and drop for inventory.
///// </summary>
//public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
//{
//    [SerializeField] private Canvas canvas;
//    private RectTransform rectTransform;
//    private CanvasGroup canvasGroup;

//    void Awake()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        canvasGroup = GetComponent<CanvasGroup>();
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        Debug.Log("OnBeginDrag");
//        canvasGroup.alpha = 0.4f;   //Transparent when moving.
//        canvasGroup.blocksRaycasts = false;
//    }
//    public void OnDrag(PointerEventData eventData)
//    {
//        Debug.Log("OnDrag");
//        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
//    }
//    public void OnEndDrag(PointerEventData eventData)
//    {
//        Debug.Log("OnEndDrag");
//        canvasGroup.alpha = 1f; //Transparent solid when done moving.
//        canvasGroup.blocksRaycasts = true;
//    }
//    public void OnPointerDown(PointerEventData eventData)
//    {
//        Debug.Log("OnPointerDown");
//    }
//}
