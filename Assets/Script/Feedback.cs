using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Feedback : MonoBehaviour {

    public GameObject NextFeedback;
    public GameController gameController;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                GoToNextFeedback();
            }
        }
        else if (Input.anyKeyDown)
        {
            GoToNextFeedback();
        }
	}
    public void GoToNextFeedback()
    {
        if (NextFeedback == null)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(gameController.GetCurrentScene() + 1);
        }
        else
        {
            NextFeedback.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
