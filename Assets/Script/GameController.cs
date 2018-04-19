﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Ball ball;
    public Button playButton;
    public Button stopButton;
    public Button resetButton;
    public Button homeButton;
    public GameObject startPanel;
    private Vector3 startLocation;
    public CircleCollider2D endArea;
    private RaycastHit2D tapped;
 
    // Use this for initialization
    void Start () {
        startLocation = ball.transform.position;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        tapped = new RaycastHit2D();
        stopButton.interactable = false;
        stopButton.image.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        foreach (var item in Input.touches)
        {
            if (item.phase == TouchPhase.Began)
            {

                tapped = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(item.position), -Vector2.up);

            }
            if (item.phase == TouchPhase.Moved && tapped.transform.CompareTag("move"))
            {
                Vector3 tapPos = new Vector3(item.position.x, item.position.y,1);
                Vector3 objPos = Camera.main.ScreenToWorldPoint(tapPos);
                tapped.transform.position = objPos;
            }
            if (item.phase == TouchPhase.Ended)
            {
                
            }
        }
        if (IsColliding())
        {
            ball.transform.position = startLocation;
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            startPanel.SetActive(true);
        }
}
    public bool IsColliding()
    {
        return ball.GetComponent<CircleCollider2D>().IsTouching(endArea);
    }

    public void EnableBall()
    {
        startPanel.SetActive(false);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playButton.interactable = false;
        playButton.image.enabled = false;
        stopButton.interactable = true;
        stopButton.image.enabled = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetBoard()
    {
        SceneManager.LoadScene(1);
    }

    //Vector3 getScreenPoint()
    //{
    //    return Camera.main.WorldToScreenPoint(new Vector3(Input.touches[0].deltaPosition.x, Input.touches[0].deltaPosition.y  screenPoint.z));

    //}

    public void TappedMachine()
    {

    }
    public void TapAndMoving()
    {

    }
}
