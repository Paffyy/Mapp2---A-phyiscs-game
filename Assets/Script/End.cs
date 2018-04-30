using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    public GameObject ball;
    public GameObject deadRat;
    public GameObject aliveRat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ball.GetComponent<CircleCollider2D>().IsTouching(this.GetComponent<CircleCollider2D>()))
        {
            deadRat.SetActive(false);
            aliveRat.SetActive(true);
        }
	}
}
