using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public Rigidbody2D ball;
    public Collider2D rocketCollider;
    public Collider2D rocketDoor;
	private Rigidbody2D rocket;
    //public SpriteRenderer ramp;
    public Vector2 rocketForce = new Vector2(0.0f, 0.5f);
    public float FlyTime;

	// Use this for initialization
	void Start () {
		rocket = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerStay2D(Collider2D coll){
			if (coll.CompareTag("Ball")){
            //ramp.enabled = false;
            Invoke("FlyRocket", 1);
            Invoke("FreeBall", FlyTime);
			}
	}

    private void FlyRocket()
    {
        ball.transform.parent = rocket.transform;
        rocketDoor.enabled = true;
        rocket.constraints = RigidbodyConstraints2D.None;
        rocket.AddForce(rocketForce, ForceMode2D.Impulse);
    }

    private void FreeBall()
    {
        rocketDoor.enabled = false;
        rocketCollider.enabled = false;
        ball.transform.parent = null;
    }


}
