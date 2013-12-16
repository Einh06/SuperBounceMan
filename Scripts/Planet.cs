using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public AudioClip boomAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Planet") {

			audio.PlayOneShot (boomAudio);
			audio.Play();
		}
	}
}
