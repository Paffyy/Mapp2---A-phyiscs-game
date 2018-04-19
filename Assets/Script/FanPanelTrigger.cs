using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPanelTrigger : MonoBehaviour {

    public GameObject settingsPanel;

	// Use this for initialization
	void Start () {
        settingsPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        settingsPanel.SetActive(true);
    }
}
