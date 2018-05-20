using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyorbelt : MonoBehaviour
{

    public GameObject conveyorbelt;
    public Rigidbody2D ball;

    private float ballMass = 0;
    public float defaultConveyorbeltSpeed = 3;
    public float conveyorbeltSpeed;
    private int Direction = 1;


    void Start()
    {
        SetSpeed();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (ball.velocity.x < conveyorbeltSpeed)
            {
                ball.velocity = new Vector2(System.Math.Abs(conveyorbeltSpeed) * Direction, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x) * (-1), transform.transform.localScale.y, transform.transform.localScale.z);
        Direction = -1;
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x), transform.transform.localScale.y, transform.transform.localScale.z);
        Direction = 1;

    }
    public void SetSpeed()
    {
        conveyorbeltSpeed = GetComponentInChildren<Machine>().GetSliderValue() + defaultConveyorbeltSpeed;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
