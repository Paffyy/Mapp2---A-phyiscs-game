using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSettingsPanel : MonoBehaviour {

    public Transform fan;

	// Use this for initialization
	void Start () {
        fan = GameObject.Find("Fan").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(fan.position.x + 1.8f, fan.position.y + 1.5f, transform.position.z);
	}


}
