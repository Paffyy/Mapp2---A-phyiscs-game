using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

    public GameObject ball;
    public GameObject FlipParent;
    public GameObject ActuallFlipParent;
    public GameController gameController;
    private Animator anim;
    private AreaEffector2D ae2d;

    // Use this for initialization
    void Start () {
        ae2d = this.GetComponent<AreaEffector2D>();
        anim = FlipParent.GetComponent<Animator>();
        anim.enabled = true;
	}

	// Update is called once per frame
	void Update () {
	    if (ball.GetComponent<CircleCollider2D>().IsTouching(this.GetComponent<BoxCollider2D>()))
        {
            anim.SetBool("Reset", false);
        }
        if (gameController.CanEdit() && !anim.GetBool("Reset"))
        {
            anim.SetBool("Reset", true);
        }
    }
    public void FlipLeft()
    {
        ActuallFlipParent.transform.localScale = new Vector3(System.Math.Abs(ActuallFlipParent.transform.localScale.x) * (-1), ActuallFlipParent.transform.localScale.y, ActuallFlipParent.transform.localScale.z);
        ae2d.forceAngle = 100;
    }
    public void FlipRight()
    {
        ActuallFlipParent.transform.localScale = new Vector3(System.Math.Abs(ActuallFlipParent.transform.localScale.x) , ActuallFlipParent.transform.localScale.y, ActuallFlipParent.transform.localScale.z);
        ae2d.forceAngle = 80;

    }
}
