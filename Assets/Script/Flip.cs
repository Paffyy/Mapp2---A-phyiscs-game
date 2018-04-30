using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

    public GameObject ball;
    private Animator anim;
    private AreaEffector2D ae2d;

	// Use this for initialization
	void Start () {
        ae2d = this.GetComponent<AreaEffector2D>();
       ae2d.forceMagnitude = 0;
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (ball.GetComponent<CircleCollider2D>().IsTouching(this.GetComponent<BoxCollider2D>()))
        {
            anim.enabled = true;
            ae2d.forceMagnitude = 50;
        }
	}
}
