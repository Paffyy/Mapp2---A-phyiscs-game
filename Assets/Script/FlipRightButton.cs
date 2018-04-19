using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipRightButton : MonoBehaviour {

    public GameObject flipRightButton;
    public GameObject flipRightButtonSelected;
    public GameObject flipLeftButtonSelected;
    public GameObject flipLeftButton;
    public GameObject fan;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("flipright");
        flipRightButton.SetActive(false);
        flipRightButtonSelected.SetActive(true);
        flipLeftButtonSelected.SetActive(false);
        flipLeftButton.SetActive(true);
        fan.GetComponent<Fan>().FlipRight();
    }
}
