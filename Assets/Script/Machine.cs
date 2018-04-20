using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    public Sprite Sprite;
    public Transform Location;
    public GameObject settingsPanel;
    private Collider2D colliderClick;
    void Start()
    {§
        HideSettingsPanel();
        Location = GetComponent<Transform>();
        colliderClick = GetComponent<Collider2D>();
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
        return new Vector3(Location.position.x + 1.8f, Location.position.y + 1.5f, 0);
    }
}
