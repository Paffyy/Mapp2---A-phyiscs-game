using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSettings : MonoBehaviour {

    public GameObject fan;
    public GameObject settingsPanel;
    public GameObject closeButton;


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

    public void FlipLeft()
    {
        fan.transform.localScale = new Vector3(fan.transform.localScale.x * -1, fan.transform.localScale.y, fan.transform.localScale.z);
    }

    public void FlipRight()
    {
        fan.transform.localScale = new Vector3(fan.transform.localScale.x * -1, fan.transform.localScale.y, fan.transform.localScale.z);
    }
}
