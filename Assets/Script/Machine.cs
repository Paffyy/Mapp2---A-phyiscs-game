﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Machine : MonoBehaviour {
    public GameObject settingsPanel;
    public Button ButtonTemplate;
    public string[] actionNames;
    public Button.ButtonClickedEvent[] actions;
    private int flip;
    private RaycastHit2D tapped;
    private Collider2D colliderClick;
    void Start()
    {
        HideSettingsPanel();
        for (int i = 0; i < actions.Length || i < actionNames.Length; i++)
        {
            AddButtonToPanel(actionNames[i], actions[i], i );
        }
        colliderClick = GetComponent<Collider2D>();
        tapped = new RaycastHit2D();
    }
    private void Update()
    {
        foreach (var item in Input.touches)
        {
            if (item.phase == TouchPhase.Began)
            {
                ShowSettingsPanel();
            }
        }
    }
    public void ShowSettingsPanel()
    {
        settingsPanel.transform.position = GetLocation();
        settingsPanel.SetActive(true);
    }
    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
    public Vector3 GetLocation()
    {
        return new Vector3(transform.position.x + 1.8f * 150, transform.position.y + 1.5f* 250, transform.position.z);
    }
    public void AddButtonToPanel(string buttonText, Button.ButtonClickedEvent actions, int i)
    {
        Button button = Instantiate(ButtonTemplate, new Vector3(0,110- 60*i), new Quaternion(0f,0f,0f,0f));
        var text = button.GetComponentInChildren<Text>();
        text.text = buttonText;
        button.onClick = actions;
        button.transform.SetParent(settingsPanel.transform, false);
    }
    public void OnMouseDown()
    {

        ShowSettingsPanel();
    }


    public void Flip()
    {
        flip = flip == 1 ? -1 : 1;
        transform.localScale = new Vector3(transform.transform.localScale.x * flip, transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(-1, transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(1, transform.transform.localScale.y, transform.transform.localScale.z);
    }
}
