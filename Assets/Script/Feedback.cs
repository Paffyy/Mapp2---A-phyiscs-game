using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour {

    public GameObject NextFeedback;
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
        }
        else
        {
            NextFeedback.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
