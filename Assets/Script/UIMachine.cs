using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMachine : MonoBehaviour, IDragHandler, IEndDragHandler {

    // Use this for initialization
    public GameObject outOfBounds;
    public GameObject prefab;
    public string prefabType;
    public GameObject UiPanel;
    public GameController gameController;
    private RectTransform rectTransform;
    private Vector3 screenPoint;
    private Vector3 startPos;
    private int UIPanelWidth = 150;
    private InstansiateMachine iMachine;
    private Text text;
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponentInChildren<Text>();
        iMachine = UiPanel.GetComponent<InstansiateMachine>();
        startPos = rectTransform.position;
        Input.multiTouchEnabled = false;
       
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (iMachine.GetCount(prefabType) > 0 && gameController.CanEdit())
        {
            text.enabled = false;
            rectTransform.position = Input.mousePosition;
            var topRight = new Vector3(rectTransform.position.x + rectTransform.sizeDelta.x / 2, rectTransform.position.y + rectTransform.sizeDelta.y / 2, rectTransform.position.z);
            var botLeft = new Vector3(rectTransform.position.x - rectTransform.sizeDelta.x / 2, rectTransform.position.y - rectTransform.sizeDelta.y / 2, rectTransform.position.z);
            if (topRight.x > Camera.main.scaledPixelWidth - (Camera.main.scaledPixelWidth / 8.7f))
            {
                outOfBounds.SetActive(true);
            }
            else
            {
                outOfBounds.SetActive(false);
            }
            if (topRight.y > Camera.main.scaledPixelHeight)
            {
                outOfBounds.SetActive(true);
            }
            if (botLeft.x < 0)
            {
                outOfBounds.SetActive(true);
            }
            if (botLeft.y < 0)
            {
                outOfBounds.SetActive(true);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (iMachine.GetCount(prefabType) > 0 && gameController.CanEdit())
        { 
            text.enabled = true;
 
            outOfBounds.SetActive(false);
            var topRight = new Vector3(rectTransform.position.x + rectTransform.sizeDelta.x / 2, rectTransform.position.y + rectTransform.sizeDelta.y / 2, rectTransform.position.z);
            var botLeft = new Vector3(rectTransform.position.x - rectTransform.sizeDelta.x / 2, rectTransform.position.y - rectTransform.sizeDelta.y / 2, rectTransform.position.z);
            if (topRight.x > Camera.main.scaledPixelWidth - (Camera.main.scaledPixelWidth / 8.7f))
            {
                rectTransform.position = startPos;
            }
            else if (topRight.y > Camera.main.scaledPixelHeight)
            {
                rectTransform.position = startPos;
            }
            else if (botLeft.x < 0)
            {
                rectTransform.position = startPos;
            }
            else if (botLeft.y < 0)
            {
                rectTransform.position = startPos;
            }
            else
            {
                iMachine.InstantiateMachine(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                iMachine.SetCount(prefabType, text);
                rectTransform.position = startPos;
            }
        }
    }
}
