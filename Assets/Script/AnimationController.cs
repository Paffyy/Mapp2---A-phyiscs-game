using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public GameObject professorRight;
    public GameObject professorLeft;
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    private bool professor;

	// Use this for initialization
	void Start () {
        professorLeft.SetActive(false);
        professor = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (rightCollider.IsTouching(professorRight.GetComponent<BoxCollider2D>())){
            Debug.Log("hej");
            professorLeft.SetActive(true);
            professorRight.SetActive(false);
       
        }

        if (leftCollider.IsTouching(professorLeft.GetComponent<BoxCollider2D>())){
            Debug.Log("hej2");
            professorRight.SetActive(true);
            professorLeft.SetActive(false);
        }
		
	}
}
