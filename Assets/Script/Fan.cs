using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour {

    public float defaultFanSpeed;
    private AreaEffector2D ae2d;
    
	// Use this for initialization
	void Start () {
        ae2d = GetComponent<AreaEffector2D>();
        ae2d.forceMagnitude = defaultFanSpeed;
	}
	
	// Update is called once per frame
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x) * (-1), transform.localScale.y, transform.localScale.z);
        ae2d.forceAngle = -180;
        GetComponentInChildren<FanRotation>().rotateDirection = 1;
        

    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        ae2d.forceAngle = 0;
        GetComponentInChildren<FanRotation>().rotateDirection = -1;

    }
    public void SetFanSpeed()
    {
        float sliderValue = GetComponentInChildren<Machine>().GetSliderValue();
        ae2d.forceMagnitude = sliderValue + defaultFanSpeed;
        GetComponentInChildren<FanRotation>().speed = (sliderValue + defaultFanSpeed)* 30;
    }
}
