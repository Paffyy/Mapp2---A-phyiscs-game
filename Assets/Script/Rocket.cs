﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public GameObject Ball;
    public GameObject RocketParent;
    public BoxCollider2D RocketCollider;
    public GameController gameController;
    private Animator anim;
    private Rigidbody2D rgbd2d;
    private bool isFlying = false;
    public GameObject machine;
    private float FlyTime;
    public float defaultFlyTime;
    private bool isLocationSet = true;
    private Vector3 start;
    private bool hasFlown = false;

    // Use this for initialization
    void Start () {
        anim = RocketParent.GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();
        anim.enabled = true;
        FlyTime = defaultFlyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            FlyTime -= Time.deltaTime;
            if (FlyTime <= 0)
            {
                ReleaseBall();
                isFlying = false;
                FlyTime = defaultFlyTime;
            }
        }
        if (!gameController.CanEdit() && isLocationSet)
        {
            isLocationSet = false;
            start = new Vector3(RocketParent.transform.position.x, RocketParent.transform.position.y, RocketParent.transform.position.z);
        }
        if (gameController.CanEdit() && !isLocationSet)
        {
            ReleaseBall();
            anim.SetBool("Reset", true);
            anim.StopPlayback();
            isFlying = false;
            isLocationSet = true;
            RocketParent.transform.localPosition = start;
            RocketParent.transform.rotation = new Quaternion(0, 0, 0, 0);
            hasFlown = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasFlown)
        {
            Ball.transform.parent = transform;
            Ball.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            isFlying = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !hasFlown)
        {
            anim.enabled = true;
            anim.SetBool("Reset", false);
        }
        if (collision.CompareTag("Platform"))
        {
            ReleaseBall();
            anim.StopPlayback();
            anim.SetBool("Reset", true);
            //rgbd2d.constraints = RigidbodyConstraints2D.None;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
    }
    public void SetFlyTime()
    {
        FlyTime = machine.GetComponent<Machine>().GetSliderValue() - 0.5f;
    }
    public void ReleaseBall()
    {
        Ball.transform.parent = null;
        hasFlown = true;
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(RocketParent.transform.transform.localScale.x) * (-1), RocketParent.transform.transform.localScale.y, RocketParent.transform.transform.localScale.z);
        anim.SetInteger("Direction", 1);
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(RocketParent.transform.transform.localScale.x), RocketParent.transform.transform.localScale.y, RocketParent.transform.transform.localScale.z);
        anim.SetInteger("Direction", 0);

    }
}
