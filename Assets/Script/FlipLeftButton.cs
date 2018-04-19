using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipLeftButton : MonoBehaviour {

    public GameObject flipLeftButton;
    public GameObject flipLeftButtonSelected;
    public GameObject flipRightButtonSelected;
    public GameObject flipRightButton;
    public GameObject fan;

	// Use this for initialization
	void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log("flipleft");
        flipLeftButton.SetActive(false);
        flipRightButtonSelected.SetActive(false);
        flipLeftButtonSelected.SetActive(true);
        flipRightButton.SetActive(true);
        fan.GetComponent<Fan>().FlipLeft();
    }
}
