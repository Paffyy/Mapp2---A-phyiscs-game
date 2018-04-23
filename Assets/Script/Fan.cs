using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    
    public GameObject fan;
    private Vector2 newVector2;
    private AreaEffector2D ae2d;
    private Vector3 offset;
    private Vector3 screenPoint;
	// Use this for initialization
	void Start () {
        ae2d = fan.GetComponent<AreaEffector2D>();
        newVector2 = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            newVector2 = fan.transform.localScale;
            newVector2.x = newVector2.x * -1f;
            fan.transform.localScale = newVector2;
            ae2d.forceAngle = 180;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ae2d.forceAngle = 0;
        }

    }

    public void FlipLeft()
    {
        fan.transform.localScale = new Vector3(fan.transform.localScale.x * -1, fan.transform.localScale.y, fan.transform.localScale.z);
        ae2d.forceAngle = 180;
    }

    public void FlipRight()
    {
        fan.transform.localScale = new Vector3(fan.transform.localScale.x * -1, fan.transform.localScale.y, fan.transform.localScale.z);
        ae2d.forceAngle = 0;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPostition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPostition;
    }
}
