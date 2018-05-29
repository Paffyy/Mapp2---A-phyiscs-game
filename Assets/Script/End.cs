using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    public GameObject ball;
    public GameObject deadRat;
    public GameObject aliveRat;
    public ParticleSystem VfxHeal;
    public AudioClip ratHeal;
    private AudioSource audioS;


    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		if (ball.GetComponent<CircleCollider2D>().IsTouching(this.GetComponent<CircleCollider2D>()))
        {
            deadRat.SetActive(false);
            aliveRat.SetActive(true);
            VfxHeal.Play();
            audioS.PlayOneShot(ratHeal, 0.1f);

        }
	}
}
