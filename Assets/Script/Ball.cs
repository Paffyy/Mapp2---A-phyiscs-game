using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody2D rgdb;

    public AudioClip bounce;
    public float volume = 0.1f;
    private AudioSource source;
    private float bounceCD = 0.3f;
    private float t;

	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(bounceCD < t)
        {
            source.PlayOneShot(bounce, volume);
            t = 0;
        }
    }


    //public void AddVelocity()
    //{
    //    rgdb.AddForce(new Vector2(6, 0));
    //}

}
