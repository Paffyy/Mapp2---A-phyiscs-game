using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    public Ball ball;
    public BoxCollider2D boxCol;
	// Use this for initialization
	void Start () {
        boxCol = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (boxCol.IsTouching(ball.GetComponent<CircleCollider2D>()))
        {
            ball.AddVelocity();
        }
	}
}
