using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public Rigidbody2D ball;
	public Rigidbody2D rocket;

	// Use this for initialization
	void Start () {
		rocket = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update () {

	}

/*
	private void OnTriggerStay2D(Collider2D coll){
			if (coll.CompareTag("ball")){
				ball.transform.parent = this.transform;
				rocket.AddForce( ForceMode.Impulse);
			}
	}

	*/
}
