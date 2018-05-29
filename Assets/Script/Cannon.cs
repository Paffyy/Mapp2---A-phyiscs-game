using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject ball;
    public GameController gameController;
    private AreaEffector2D ae2d;

    // Use this for initialization
    void Start()
    {
        ae2d = this.GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (ball.GetComponent<CircleCollider2D>().IsTouching(this.GetComponent<CircleCollider2D>()))
        //{
        //    ball.GetComponent<SpriteRenderer>().enabled = false;
        //    ae2d.enabled = true;
        //}
    }
    public void FlipLeft()
    {
        this.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Math.Abs(this.transform.rotation.z) * -1, this.transform.rotation.w);
        ae2d.forceAngle = 135;
    }
    public void FlipRight()
    {
        this.transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, Math.Abs(this.transform.rotation.z), this.transform.rotation.w);
        ae2d.forceAngle = 45;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball.GetComponent<SpriteRenderer>().enabled = true;
            ae2d.enabled = false;
  
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball.GetComponent<SpriteRenderer>().enabled = false;
            ae2d.enabled = true;
        }
    }

}
