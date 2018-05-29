using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public GameObject Ball;
    public GameObject RocketParent;
    public BoxCollider2D RocketCollider;
    public GameController gameController;
    public float defaultFlyTime;
    public GameObject machine;
    public AudioClip rocketSound;
    public ParticleSystem rocketVFX;



    private AudioSource audioS;
    private Animator anim;
    private Rigidbody2D rgbd2d;
    private bool isFlying = false;
    private float FlyTime;
    private bool isLocationSet = true;
    private Vector3 start;
    private bool hasFlown = false;
    private bool hasEntered = false;



    // Use this for initialization
    void Start () {
        anim = RocketParent.GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();
        anim.enabled = true;
        SetFlyTime();
        audioS = GetComponent<AudioSource>();

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
                SetFlyTime();
                isFlying = false;
                hasEntered = false;
            }
        }
        if (!gameController.CanEdit() && isLocationSet)
        {
            isLocationSet = false;
            start = new Vector3(RocketParent.transform.position.x, RocketParent.transform.position.y, RocketParent.transform.position.z);
        }
        if (gameController.CanEdit() && !isLocationSet)
        {
            Ball.transform.parent = null;
            anim.SetBool("Reset", true);
            anim.StopPlayback();
            isFlying = false;
            isLocationSet = true;
            RocketParent.transform.localPosition = start;
            RocketParent.transform.rotation = new Quaternion(0, 0, 0, 0);
            hasFlown = false;
            hasEntered = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Ball") && !gameController.CanEdit() && hasEntered) 
        {
            Ball.transform.parent = transform;
            Ball.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            isFlying = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameController.CanEdit())
        {
            if (collision.CompareTag("Ball") && !hasFlown)
            {
                anim.enabled = true;
                anim.SetBool("Reset", false);
                hasEntered = true;
                audioS.PlayOneShot(rocketSound, 0.2f);
                rocketVFX.Play();
                Debug.Log("dingdon");

            }
            if (collision.CompareTag("Platform"))
            {
                ReleaseBall();
                anim.StopPlayback();
                anim.SetBool("Reset", true);
                hasEntered = false;
                //rgbd2d.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
    public void SetFlyTime()
    {
        FlyTime = defaultFlyTime + machine.GetComponent<Machine>().GetSliderValue() - 0.5f;
    }
    public void ReleaseBall()
    {
        Ball.transform.parent = null;
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(RocketParent.transform.transform.localScale.x) * (-1), RocketParent.transform.transform.localScale.y, RocketParent.transform.transform.localScale.z);
        anim.SetInteger("Direction", 1);
        RocketParent.GetComponent<SpriteRenderer>().flipX = false;

    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(RocketParent.transform.transform.localScale.x), RocketParent.transform.transform.localScale.y, RocketParent.transform.transform.localScale.z);
        RocketParent.GetComponent<SpriteRenderer>().flipX = true;
        anim.SetInteger("Direction", 0);

    }
}
