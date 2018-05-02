using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSound : MonoBehaviour {

	public AudioClip sound;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerStay2D(Collider2D col){
		if(col.CompareTag("Ball")){
			if(!audioSource.isPlaying)
				audioSource.PlayOneShot(sound, 0.5f);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(audioSource.isPlaying)
			audioSource.Stop();

	}
}
