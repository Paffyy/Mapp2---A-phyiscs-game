using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyorbelt : MonoBehaviour {

	public GameObject conveyorbelt;
	public Rigidbody2D ball;

	private float ballMass = 0;

	public int conveyorbeltSpeed = 1;

	// Use this for initialization
	void Start () {
		//ballMass = ball.mass * 2;

	}

	private void OnTriggerStay2D (Collider2D collision){
		if (collision.CompareTag("Ball")){
			//ball.mass = ballMass;
            Debug.Log(ball.velocity);
			if (ball.velocity.x < conveyorbeltSpeed){
				ball.velocity = new Vector2(conveyorbeltSpeed,0);
			}
		}
	}
    
    private void OnTriggerExit2D (Collider2D collision)
    {
        //Debug.Log(ball.velocity.x);
        //ball.mass = ballMass/2;
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x) * (-1), transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x), transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void SetSpeed()
    {

    }
    // Update is called once per frame
    void Update () {

	}
}
