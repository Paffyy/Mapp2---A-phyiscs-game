using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerCollider : MonoBehaviour {

    public GameObject Rocket;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Debug.Log("test");
            Rocket.GetComponent<Rocket>().Crash();
        }
    }
}
