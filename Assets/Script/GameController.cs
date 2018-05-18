using System.Collections;
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
    public GameObject startLocationArea;
    public int currentScene;
    public int levelCount;
    public enum gameState
    {
        EDIT,
        PLAY,
        WON
    }
    public gameState currentState;
    private RaycastHit2D tapped;
 
    // Use this for initialization
    void Start () {
        startLocation = new Vector3(ball.transform.position.x, ball.transform.position.y, 1);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        tapped = new RaycastHit2D();
        if (stopButton != null)
        {
            stopButton.interactable = false;
            stopButton.image.enabled = false;
        }
        currentState = gameState.EDIT;
    }

    // Update is called once per frame
    void Update () {

        if (ball.GetComponent<Collider2D>().IsTouching(startLocationArea.GetComponent<Collider2D>()))
        {
            currentState = gameState.EDIT;
        }
        else
        {
            currentState = gameState.PLAY;
        }
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
            
            if (currentScene + 1 > levelCount)
            {
                ReturnToMenu();
            }
            else
            {
                SceneManager.LoadScene(currentScene + 1);
            }
        }
    }
    public bool IsColliding()
    {
        return ball.GetComponent<CircleCollider2D>().IsTouching(endArea);
    }

    public bool CanEdit()
    {
        return currentState == gameState.EDIT;
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

    public void ResetBall()
    {
        ball.transform.position = startLocation;
        ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        stopButton.interactable = false;
        stopButton.image.enabled = false;
        playButton.interactable = true;
        playButton.image.enabled = true;

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetBoard()
    {
        SceneManager.LoadScene(currentScene);
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
