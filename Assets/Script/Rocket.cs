using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public GameObject Ball;
    public float FlyTime;
    public GameObject RocketParent;
    public BoxCollider2D RocketCollider;
    private Animator anim;
    private Rigidbody2D rgbd2d;

	// Use this for initialization
	void Start () {
        anim = RocketParent.GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

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
        if (collision.CompareTag("Platform"))
        {
          Ball.transform.parent = null;
          anim.StopPlayback();
          anim.enabled = false;
          rgbd2d.constraints = RigidbodyConstraints2D.None;
          RocketCollider.enabled = false;

        }
    }

    public void OnTriggerExit2D(Collider2D collision){
    }

    public void Crash()
    {
        //Debug.Log("1");
        //Debug.Log("2");
        //Debug.Log("3");
        //Debug.Log("4");
        //Debug.Log("5");
    }

    private void FreeBall()
    {
        //   rocketDoor.enabled = false;
       // rocketCollider.enabled = false;
      //  ball.transform.parent = null;
    }


}
