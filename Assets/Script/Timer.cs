using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public float timeToFinish;
    public bool isLevelTimed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartLevelTimer(isLevelTimed);
	}
    public void StartLevelTimer(bool isLevelTimed)
    {
        if(isLevelTimed)
        {
            timeToFinish -= Time.deltaTime;
            if (timeToFinish < 0)
            {
                Debug.Log("end");
            }
        }
    }
}
