using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public GameObject Ball;
    public float FlyTime;
    public GameObject RocketParent;
    public BoxCollider2D RocketCollider;
    public BoxCollider2D RocketColliderFront;
    public BoxCollider2D PlatformCollider;
    private Animator anim;
    private Rigidbody2D rgbd2d;

	// Use this for initialization
	void Start () {
        anim = RocketParent.GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
        if (RocketColliderFront.IsTouchingLayers())
        {
            Debug.Log("test");
            RocketCollider.enabled = false;
            anim.StopPlayback();
            anim.enabled = false;
            Ball.transform.parent = null;
            rgbd2d.constraints = RigidbodyConstraints2D.None;
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
     if (other.CompareTag("Ball"))
        {
            Ball.transform.parent = transform;
            Ball.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            anim.enabled = true;
            anim.Play("Rocket");
        }
    }

    public void Crash()
    {
        RocketCollider.enabled = false;
        anim.StopPlayback();
        anim.enabled = false;
        Ball.transform.parent = null;
        rgbd2d.constraints = RigidbodyConstraints2D.None;
    }


    private void FreeBall()
    {
        //   rocketDoor.enabled = false;
       // rocketCollider.enabled = false;
      //  ball.transform.parent = null;
    }


}
