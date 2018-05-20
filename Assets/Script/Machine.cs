﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Machine : MonoBehaviour
{
    public GameObject settingsPanel;
    public Canvas canvas;
    public Button ButtonTemplate;
    public GameController gameController;
    public string[] actionNames;
    public Button.ButtonClickedEvent[] actions;
    public Slider sliderTemplate;
    public string sliderNames;
    public Slider.SliderEvent sliderEvents;
    public int minValue;
    public int maxValue;
    public GameObject outOfBounds;
    private float sliderValue;
    private GameObject panel;
    private Vector3 offset;
    private int tapCount = 0;
    private int settingsPanelWidth = 400;
    private int settingsPanelHeight = 400;
    private int latestButtonY = 170;
    private int UIPanelWidth = 150;
    private SpriteRenderer _sprite;
    private RaycastHit2D tapped;
    private float tapDelay;
    private bool MachineWasHit = false;
    private bool IsClicking = false;
    private Transform machine;

    void Start()
    {
        _sprite = GetComponentInParent<SpriteRenderer>();
        machine = transform.parent;
        outOfBounds.SetActive(false);
    }
    private void Update()
    {
        if (Input.touchCount > 0 && gameController.CanEdit())
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                tapped = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), -Vector2.up);
                if (tapped.transform.CompareTag("Machine") && tapped.transform.GetComponentInChildren<GameObject>().transform.CompareTag("Machine"))
                {
                    tapCount++;
                    if (tapDelay == -1)
                    {
                        tapDelay = 0;
                    }
                    MachineWasHit = true;
                }
                else
                {
                    MachineWasHit = false;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && MachineWasHit)
            {
                if (panel != null)
                {
                    HideSettingsPanel(panel);
                }
                var touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                if (tapped.transform.CompareTag("Machine"))
                {
                    tapped.transform.position = new Vector3(touch.x, touch.y, machine.transform.position.z);
                }
                CheckIfOutside();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended && MachineWasHit)
            {
                // if outside, put in the middle
                var topRight = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x + _sprite.bounds.size.x / 2, machine.transform.position.y + _sprite.bounds.size.y / 2, machine.transform.position.z));
                var botLeft = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x - _sprite.bounds.size.x / 2, machine.transform.position.y - _sprite.bounds.size.y / 2, machine.transform.position.z));
                if (topRight.x > Camera.main.scaledPixelWidth - UIPanelWidth)
                {
                    machine.transform.position = new Vector3(0, 0);
                    HideSettingsPanel(panel);
                }
                if (topRight.y > Camera.main.scaledPixelHeight)
                {
                    machine.transform.position = new Vector3(0, 0);
                    HideSettingsPanel(panel);
                }
                if (botLeft.x < 0)
                {
                    machine.transform.position = new Vector3(0, 0);
                    HideSettingsPanel(panel);
                }
                if (botLeft.y < 0)
                {
                    machine.transform.position = new Vector3(0, 0);
                    HideSettingsPanel(panel);
                }
                if (panel != null)
                {
                    panel.SetActive(true);
                    panel.transform.position = GetLocation();
                }
                // if double tapped
                if (panel == null && tapCount >= 2 && tapDelay > 0.25)
                {
                    if (tapped.transform.CompareTag("Machine"))
                    {
                        ShowSettingsPanel();
                    }
                }
                outOfBounds.SetActive(false);
            }
        }
        if ((MachineWasHit && tapDelay != -1) || (IsClicking && tapDelay != -1))
        {
            tapDelay += Time.deltaTime;
        }
        if (tapDelay > 0.5f)
        {
            ResetTap();
        }
    }

    private void ResetTap()
    {
        tapCount = 0;
        tapDelay = -1;
    }

    private void CheckIfOutside()
    {
        var topRight = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x + _sprite.bounds.size.x / 2, machine.transform.position.y + _sprite.bounds.size.y / 2, machine.transform.position.z));
        var botLeft = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x - _sprite.bounds.size.x / 2, machine.transform.position.y - _sprite.bounds.size.y / 2, machine.transform.position.z));
        if (topRight.x > Camera.main.scaledPixelWidth - UIPanelWidth)
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

    public void ShowSettingsPanel()
    {
        ResetTap();
        var panel1 = Instantiate(settingsPanel);
        for (var i = 0; i < actions.Length && i < actionNames.Length; i++)
        {
            AddButtonToPanel(actionNames[i], actions[i], panel1);
        }

        if (sliderEvents.GetPersistentEventCount() > 0 && !sliderNames.Equals(""))
        {
            AddSliderToPanel(sliderNames, panel1, sliderEvents, minValue, maxValue);
        }
        panel1.transform.SetParent(canvas.transform, false);
        panel1.transform.position = GetLocation();
        panel1.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            HideSettingsPanel(panel1);
        });
        panel1.SetActive(true);
        panel = panel1;
        if (sliderEvents.GetPersistentEventCount() > 0 && !sliderNames.Equals(""))
        {
            panel.GetComponentInChildren<Slider>().value = sliderValue;
        }
    }
    public void HideSettingsPanel(GameObject panel1)
    {
        if (panel1 != null)
        {
            Destroy(panel1);
            latestButtonY = 170;
        }
    }

    public void AddButtonToPanel(string buttonText, Button.ButtonClickedEvent actions, GameObject panel1)
    {
        Button button = Instantiate(ButtonTemplate, new Vector3(0, latestButtonY - 60), new Quaternion(0f, 0f, 0f, 0f));
        var text = button.GetComponentInChildren<Text>();
        text.text = buttonText;
        button.onClick = actions;
        button.transform.SetParent(panel1.transform, false);
        latestButtonY -= 60;
    }

    public void AddSliderToPanel(string sliderName, GameObject panel1, Slider.SliderEvent sliderEvents, int minValue, int maxValue)
    {
        Slider slider = Instantiate(sliderTemplate, new Vector3(78, latestButtonY - 60), new Quaternion(0f, 0f, 0f, 0f));
        slider.GetComponentInChildren<Text>().text = sliderNames;
        slider.transform.SetParent(panel1.transform, false);
        slider.wholeNumbers = true;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.onValueChanged = sliderEvents;
        latestButtonY -= 60;
    }
    void OnMouseDown()
    {
        if (gameController.CanEdit())
        {
            var screenPoint = Camera.main.WorldToScreenPoint(machine.transform.position);
            offset = machine.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            tapCount++;
            if (tapDelay == -1)
            {
                tapDelay = 0;
            }
            IsClicking = true;
        }
    }
    void OnMouseDrag()
    {
        if (gameController.CanEdit())
        {
            if (panel != null)
            {
                HideSettingsPanel(panel);
            }
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 curPostition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            machine.transform.position = curPostition;
            CheckIfOutside();
        }
    }
    void OnMouseUp()
    {
        // if outside, put in the middle
        var topRight = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x + _sprite.bounds.size.x / 2, machine.transform.position.y + _sprite.bounds.size.y / 2, machine.transform.position.z));
        var botLeft = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x - _sprite.bounds.size.x / 2, machine.transform.position.y - _sprite.bounds.size.y / 2, machine.transform.position.z));

        if (topRight.x > Camera.main.scaledPixelWidth - UIPanelWidth)
        {
            machine.transform.position = new Vector3(0, 0);
            HideSettingsPanel(panel);
        }
        if (topRight.y > Camera.main.scaledPixelHeight)
        {
            machine.transform.position = new Vector3(0, 0);
            HideSettingsPanel(panel);
        }
        if (botLeft.x < 0)
        {
            machine.transform.position = new Vector3(0, 0);
            HideSettingsPanel(panel);
        }
        if (botLeft.y < 0)
        {
            machine.transform.position = new Vector3(0, 0);
            HideSettingsPanel(panel);
        }
        if (panel != null)
        {
            panel.SetActive(true);
            panel.transform.position = GetLocation();
        }
        // if double tapped
        if (panel == null && tapCount >= 2 && tapDelay > 0.15)
        {
            ShowSettingsPanel();
        }
        outOfBounds.SetActive(false);
    }
    //Returns location of the top right corner of the sprite converted to UI coordinates
    public Vector3 GetLocation()
    {
        // Gets all corners of the machine sprite
        var topRight = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x + _sprite.bounds.size.x / 2, machine.transform.position.y + _sprite.bounds.size.y / 2, machine.transform.position.z));
        var botRight = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x + _sprite.bounds.size.x / 2, machine.transform.position.y - _sprite.bounds.size.y / 2, machine.transform.position.z));
        var topLeft = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x - _sprite.bounds.size.x / 2, machine.transform.position.y + _sprite.bounds.size.y / 2, machine.transform.position.z));
        var botLeft = Camera.main.WorldToScreenPoint(new Vector3(machine.transform.position.x - _sprite.bounds.size.x / 2, machine.transform.position.y - _sprite.bounds.size.y / 2, machine.transform.position.z));
        var location = topRight;

        // Checks if the panel would appear outside in any direction

        if (topRight.y > Camera.main.scaledPixelHeight) // outside top
        {
            location = new Vector3(topRight.x, botRight.y);

        }
        if (topLeft.y - settingsPanelHeight < 0) // outside bot
        {
            location.y = topLeft.y + settingsPanelHeight / 2;
            if (topLeft.y - settingsPanelHeight / 2 < 0)
            {
                location.y = botRight.y + settingsPanelHeight;
                if (botLeft.y < 0)
                {
                    location.y = topRight.y + settingsPanelHeight;
                }
            }
        }
        if (topRight.x + settingsPanelWidth > Camera.main.pixelWidth) // outside right
        {
            location.x = topLeft.x - settingsPanelWidth;
        }
        if (topLeft.x - settingsPanelWidth < 0) // outside left
        {
            location.x = topRight.x;
        }
        return location;
    }
    public void SetSliderValue()
    {
        sliderValue = panel.GetComponentInChildren<Slider>().value;
    }
    public float GetSliderValue()
    {
        return sliderValue;
    }

    #region Old Events

    //void OnMouseDown()
    //{
    //    if (panel == null)
    //    {
    //        ShowSettingsPanel();
    //    }
    //    /* else if(settingsPanel.activeSelf)            försökte får så att man kunde gömma settings panel om man klickar en gång utan att lyckas
    //    {
    //        settingsPanel.SetActive(false);
    //    }*/
    //    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    //    offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    //}
    //void OnMouseDrag()
    //{
    //    heldDownTime += Time.deltaTime;
    //    if (heldDownTime > 0.6f)
    //    {
    //        panel.SetActive(false);
    //    }
    //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
    //    Vector3 curPostition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    //    transform.position = curPostition;
    //}
    //void OnMouseUp()
    //{
    //    if (panel != null)
    //    {
    //        panel.SetActive(true);
    //        panel.transform.position = GetLocation();
    //    }
    //}
    //// Returns location of the top right corner of the sprite converted to UI coordinates
    //public Vector3 GetLocation()
    //{
    //    machine
    //    return Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z));
    //}
    //public void Flip()
    //{
    //    flip = flip == 1 ? -1 : 1;
    //    transform.localScale = new Vector3(transform.transform.localScale.x * flip, transform.transform.localScale.y, transform.transform.localScale.z);
    //}
    //public void FlipLeft()
    //{
    //    transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x) * (-1), transform.transform.localScale.y, transform.transform.localScale.z);
    //}
    //public void FlipRight()
    //{
    //    transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x), transform.transform.localScale.y, transform.transform.localScale.z);
    //}
    //public void FanSpeed()
    //{
    //    fanSpeed = panel.GetComponentInChildren<Slider>().value;
    //    Debug.Log(fanSpeed);
    //}
    #endregion

}
